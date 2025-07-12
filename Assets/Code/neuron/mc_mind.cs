using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixify;
using System;
using UnityEngine.PlayerLoop;

namespace Triheroes.Code
{
    public class mc_mind : module
    {
        [Depend]
        m_cortex mc;

        AtomPool<mind_fragment> pool;
        List<mind_fragment> ActiveFragments = new List<mind_fragment>();

        public override void Create()
        {
            pool = new AtomPool<mind_fragment>(null, null, PoolAtomInit);
        }

        public void DoTask (SuperKey task)
        {
            var frag = pool.GetAtom();
            frag.Do(task);

            if (frag.header.Length > 0)
            ActiveFragments.Add(frag);

            CleanUp ();
        }

        void PoolAtomInit (mind_fragment frag)
        {
            frag.host = mc;
        }

        public bool IsTaskRunning (SuperKey task)
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
                if (ActiveFragments[i].header.Length == 0)
                    ActiveFragments.RemoveAt(i);
            }
        }

        class mind_fragment : atom
        {
            public m_cortex host;
            public plan_header header { get; private set; }
            neuron neuron;

            public mind_fragment()
            {
                header = new plan_header();
                neuron = new neuron();
                neuron.OnNeuronEnd = NeuronEnd;
            }

            public void Do(SuperKey task)
            {
                header.Clear();

                var preparation = Prepare(task);

                if (!preparation)
                {
                    // failed
                    header.Clear();
                    return;
                }
                Update();
            }

            bool Prepare(SuperKey task)
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

            void Update()
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
                    neuron.Aquire(this, header[0].main);
                }
            }

            void NeuronEnd()
            {
                header.Dequeue();
                Update();
            }
        }

    }

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

    public class plan_notion
    {
        public SuperKey descriptor { get; private set; }
        public action main { get; private set; }
        public Notion[] notion { get; private set; }

        public plan_notion(Notion[] notion, action action, SuperKey descriptor)
        {
            this.notion = notion;
            main = action;
            this.descriptor = descriptor;
        }
    }

    public class Notion
    {
        Func<bool> Problem;
        public SuperKey Solution { get; private set; }

        public bool Check() => Problem();
        bool DefProblem() => true;

        public Notion(Func<bool> Problem, SuperKey Solution)
        {
            this.Problem = Problem ?? DefProblem;
            this.Solution = Solution;
        }

        public Notion()
        {
            Problem = DefProblem;
            Solution = new SuperKey("zero");
        }

        public Notion(SuperKey Solution)
        {
            Problem = DefProblem;
            this.Solution = Solution;
        }
    }

}