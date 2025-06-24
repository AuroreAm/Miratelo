using Pixify;
using static Pixify.treeBuilder;

namespace Triheroes.Code
{
    public static class PlayerControllerLibrary
    {
        public static action MainPlayer ()
        {
            new selector () {fallback = true};

                new parallel () { Tag = StateKey2.zero };
                    new selector () {fallback = true};
                        new parallel () { Tag = StateKey2.move };
                            new pc_move ();
                            new t_fall ();
                            new t_jump_complex ();
                                new selector () { fallback = true };
                                    new parallel () { Tag = StateKey2.zero };
                                        new t_draw ();
                                        new t_return ();
                                    end ();   
                                    new ac_draw_weapon () { Tag = StateKey2.draw };
                                    new ac_return_weapon () { Tag = StateKey2.return_};
                                end ();
                        end ();
                        new parallel () { Tag = StateKey2.jump };
                            new pc_jump ();
                            new to_move ();
                            new t_fall ();
                        end ();
                        new pc_fall () { Tag = StateKey2.fall };
                    end ();
                    new t_mwu ();
                end ();

                new parallel () { Tag = StateKey2.msu };
                    new selector () {fallback = true};
                        new parallel () { Tag = StateKey2.move };
                            new selector () {fallback = true, reset = false};
                                new parallel () { Tag = StateKey2.zero };
                                    new pc_move ();
                                    new t_target ();
                                end ();
                                new parallel () {Tag = StateKey2.targetting};
                                    new pc_lateral_move ();
                                    new ac_lock_target ();
                                    new pcc_target_target ();
                                    new t_untarget ();
                                end ();
                            end ();
                            new t_fall ();
                            new t_jump_complex ();
                            new t_slash ();
                            new selector () { fallback = true };
                                new parallel () { Tag = StateKey2.zero };
                                    new t_return ();
                                    new t_swap_sword ();
                                end ();
                                new ac_return_weapon () { Tag = StateKey2.return_};
                                new sequence () { Tag = t_swap_sword.sword_swap, repeat = false };
                                    new ac_return_weapon ();
                                    new ac_draw_weapon ();
                                end ();
                            end ();
                        end ();
                        new parallel () { Tag = StateKey2.jump };
                            new pc_jump ();
                            new to_move ();
                            new t_fall ();
                        end ();
                        new pc_fall () { Tag = StateKey2.fall };
                        new controlled_sequence () { Tag = StateKey2.slash };
                            new parallel () { StopWhenFirstNodeStopped = true };
                                new ac_slash () { ComboId = 0 };
                                new t_combo_success ();
                            end ();
                            new parallel () { StopWhenFirstNodeStopped = true };
                                new ac_slash () { ComboId = 1 };
                                new t_combo_success ();
                            end ();
                            new parallel () { StopWhenFirstNodeStopped = true };
                                new ac_slash () { ComboId = 2 };
                                new t_combo_success ();
                            end ();
                        end ();
                    end ();
                    new t_zero ();
                    new pc_target ();
                end ();

                new parallel () { Tag = StateKey2.mbu };
                    new selector () {fallback = true};
                        new parallel () { Tag = StateKey2.move };
                            new pc_move ();
                            new t_fall ();
                            new t_jump_complex ();
                                new selector () { fallback = true };
                                    new parallel () { Tag = StateKey2.zero };
                                        new t_return ();
                                        new t_aim ();
                                    end ();
                                    new ac_return_weapon () { Tag = StateKey2.return_};
                                    new pc_aim () { Tag = StateKey2.aim };
                                end ();
                        end ();
                        new parallel () { Tag = StateKey2.jump };
                            new pc_jump ();
                            new to_move ();
                            new t_fall ();
                        end ();
                        new pc_fall () { Tag = StateKey2.fall };
                    end ();
                    new t_zero ();
                end ();

            end ();

            return TreeFinalize ();
        }
    }
}