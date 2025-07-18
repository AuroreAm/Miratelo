using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pixify.Spirit
{

    public class s_mind : pix
    {
        #region reflexion and cortex
        List <reflexion> Reflexions = new List<reflexion> ();
        List <int> ReflexionKeys = new List<int> ();

        cortex cortex;

        public void SetCortex ( cortex newCortex )
        {
            cortex = newCortex;
            b.IntegratePix (newCortex);
            TriggerThinking ();
        }

        public void TriggerThinking ()
        {
            ClearReflexion ();
            cortex.Think ();
        }

        public void AddReflexion ( reflexion r )
        {
            if (Reflexions.Contains (r))
                return;
            ReflexionKeys.Add ( Stage.Start ( r ) );
            Reflexions.Add (r);
        }

        public void ClearReflexion ()
        {
            foreach ( var i in ReflexionKeys )
            Stage.Stop ( i );

            Reflexions.Clear ();
            ReflexionKeys.Clear ();
        }
        #endregion

        #region plan and notion
        Dictionary <term, plan_notion> Notion = new Dictionary <term, plan_notion> ();
        public plan_notion GetPlan ( term key )
        {
            if (Notion.ContainsKey (key))
                return Notion [key];
            else
                return null;
        }
        public void AddNotion ( plan_notion notion )
        {
            b.IntegratePix ( notion );
            Notion.Add (notion.descriptor, notion);
        }
        #endregion

        #region task
        
        PixPool <mind_fragment> pool;
        List <mind_fragment> ActiveFragments = new List<mind_fragment> ();

        public override void Create()
        {
            pool = new PixPool<mind_fragment>(null, null, PoolAtomInit);
        }

        void PoolAtomInit (mind_fragment frag)
        {
            frag.host = this;
        }

        public void DoTask (term task)
        {
            CleanUp ();

            if ( ActiveFragments.Exists(f => f.Descriptor == task) )
            return;

            var frag = pool.RentPix();
            frag.Do(task);

            if (frag.on)
            {
                frag.Descriptor = task;
                ActiveFragments.Add(frag);
            }
        }

        public bool IsTaskRunning (term task)
        {
            CleanUp ();
            foreach (var f in ActiveFragments)
                if (f.header[0].descriptor == task)
                    return true;
            return false;
        }

        void CleanUp ()
        {
            for (int i = ActiveFragments.Count - 1; i >= 0; i--)
            {
                if (!ActiveFragments[i].on)
                    ActiveFragments.RemoveAt(i);
            }
        }


        class mind_fragment : spirit, IPixiHandler
        {
            public s_mind host;
            public plan_header header { get; private set; } = new plan_header();
            public term Descriptor;

            task main;

            public void Do(term task)
            {
                header.Clear();

                
                var p = host.GetPlan(task);
                if (p == null) return;

                header.WritePlan ( p );
                PrepareCurrentTask();

                if ( main != null )
                Stage.Start (this);
            }

            protected override void Step()
            {
                main.Tick (this);
            }
            
            public void OnPixiEnd ( pixi p )
            {
                header.Dequeue();
                main = null;
                PrepareCurrentTask();

                if ( main == null )
                SelfStop ();
            }

            bool Prepare(term task)
            {
                var p = host.GetPlan(task);
                if (p == null) return false;

                foreach (var n in p.notion)
                {
                    if (!n.Check())
                    {
                        if (!Prepare(n.Solution))
                            return false;
                    }
                }

                header.WritePlan(p);
                return true;
            }

            void PrepareCurrentTask()
            {
                if (header.Length > 0)
                {
                    header.cursor = 0;
                    foreach (var n in header[0].notion)
                    {
                        if (!n.Check())
                        {
                            if (!Prepare(n.Solution))
                            {
                                header.Clear();
                                return;
                            }
                        }
                    }
                    main = header [0].main;
                }
            }
        }

        #endregion

    }


    //-----------------------------------------------------
    public class plan_header
    {
        List<plan_notion> plan = new List<plan_notion>();

        public int cursor
        {
            get { return Mathf.Clamp(_cursor, 0, plan.Count); }
            set { _cursor = Mathf.Clamp(value, 0, plan.Count); ; }
        }
        int _cursor;

        public int Length => plan.Count;

        public void Dequeue() => plan.RemoveAt(0);

        public void Clear()
        {
            plan.Clear();
        }

        public plan_notion this[int i] => plan[i];

        public void SetCursor(int i) => cursor = i;

        public void WritePlan(plan_notion p)
        {
            if (cursor == plan.Count)
                plan.Add(p);
            else
                plan.Insert(cursor, p);

            cursor++;
        }
    }


    //-----------------------------------------------------
    public class plan_notion : pix
    {
        public term descriptor { get; private set; }
        public task main { get; private set; }
        public Notion[] notion { get; private set; }

        public override void Create()
        {
            b.IntegratePix ( main );
        }

        public plan_notion(Notion[] notion, task action, term descriptor)
        {
            this.notion = notion;
            main = action;
            this.descriptor = descriptor;
        }

        public plan_notion (Notion notion, task action, term descriptor)
        {
            this.notion = new Notion[] { notion };
            main = action;
            this.descriptor = descriptor;
        }

        public plan_notion ( task action, term descriptor)
        {
            this.notion = new Notion[0];
            main = action;
            this.descriptor = descriptor;
        }
    }

    //-----------------------------------------------------
    public class Notion
    {
        Func<bool> Problem;
        public term Solution;

        public bool Check() => Problem();
        bool DefProblem() => true;

        public Notion(Func<bool> Problem, term Solution)
        {
            this.Problem = Problem ?? DefProblem;
            this.Solution = Solution;
        }

        public Notion()
        {
            Problem = DefProblem;
            Solution = new term("zero");
        }

        public Notion(term Solution)
        {
            Problem = DefProblem;
            this.Solution = Solution;
        }
    }

}