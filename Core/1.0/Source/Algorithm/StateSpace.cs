using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm
{
    namespace StateSpace
    {
        public class D<T> where T : IModel<T>, new()
        {
            public S<T> Status0 { get; set; }
            public List<S<T>> Goals { get; set; }
            public List<S<T>> Status { get; set; }
            public List<A<T>> Actions { get; set; }

            public  List<List<S<T>>> Do()
            {
                S<T> now = Status0;
                List<S<T>> list = new List<S<T>>();
                bool success = true;
                int index = 0;
                list.Add(now);

                List<List<S<T>>> results = new List<List<S<T>>>();

                List<S<T>> endPoints = new List<S<T>>();

                while (true)
                {
                    if (now.IsSuccess(Goals))
                    {
                        endPoints.Add(now);
                    }
                    else
                    {
                        Actions.ForEach(a =>
                        {
                            S<T> next = a.Act(now);
                            if (next != null && !list.Contains(next))
                            {
                                next.Parent = now;
                                next.ParentAction = a;
                                list.Add(next);
                            }
                        });
                    }
                    if (index == list.Count - 1)
                    {
                        break;
                    }
                    now = list[++index];
                }
                if (endPoints.Count>0)
                {
                    endPoints.ForEach(p =>
                    {
                        List<S<T>> result = new List<S<T>>();
                        Stack<S<T>> stack = new Stack<S<T>>();
                        while (now.Parent != null)
                        {
                            stack.Push(now);
                            now = now.Parent;
                        }
                        stack.Push(now);

                        while (stack.Count > 0)
                            result.Add(stack.Pop());

                        results.Add(result);
                    });
                   
                }
                return results;
            }
        }


        public class S<T> where T : IModel<T>, new()
        {
            public T Model { get; set; }
            public S<T> Parent { get; set; }
            public A<T> ParentAction { get; set; }

            public S(T model)
            {
                this.Model = model;
            }

            public virtual bool IsSuccess(S<T> goal)
            {
                return this.Model.Equals(goal.Model);
            }

            public virtual bool IsSuccess(List<S<T>> goals)
            {
                return goals.FirstOrDefault(s => s.Model.Equals(this.Model)) != null;
            }

            public override bool Equals(object obj)
            {
                S<T> ss = obj as S<T>;
                if (ss == null)
                {
                    return false;
                }
                return this.Model.Equals(ss.Model);
            }
        }

        public class A<T> where T : IModel<T>, new()
        {
            public T Vector { get; set; }

            public List<S<T>> SArea { get; set; }

            public A(T vector)
            {
                this.Vector = vector;
            }

            public virtual S<T> Act(S<T> s)
            {
                if (!s.Model.Validate(this))
                {
                    return null;
                }
                S<T> next = new S<T>(s.Model.MoveTo(this.Vector));
                if (!this.SArea.Contains(next))
                {
                    return null;
                }
                return next;
            }
        }

        public interface IModel<T> where T : IModel<T>, new()
        {
            T MoveTo(T vector);
            bool Validate(A<T> action);
        }

    }

}
