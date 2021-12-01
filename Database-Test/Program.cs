using Database_Test.Database;
using Database_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Database_Test
{
    class Program
    {
        public static void Add(Parent parent, TestDatabase context)
        {
            GrandChild grandchild = new GrandChild();
            Child child = new Child();
            if (parent.Child != null)
            {
                if (parent.Child.GrandChild != null)
                {
                    if (context.GrandChildDatabase.Where(a => a.Id == parent.Child.GrandChild.Id).FirstOrDefault() == null)
                    {
                        parent.Child.GrandChild.Id = 0;
                        context.GrandChildDatabase.Add(parent.Child.GrandChild);
                        context.SaveChanges();
                        grandchild = context.GrandChildDatabase.OrderBy(a => a.Id).Last();
                        context.SaveChanges();
                    }
                    else
                    {
                        grandchild = context.GrandChildDatabase.Where(a => a.Id == parent.Child.GrandChild.Id).FirstOrDefault();
                    }
                }

                if (context.ChildDatabase.Where(a => a.Id == parent.Child.Id).FirstOrDefault() == null)
                {
                    parent.Child.Id = 0;
                    parent.Child.GrandChild = grandchild;
                    context.ChildDatabase.Add(parent.Child);
                    context.SaveChanges();
                    child = context.ChildDatabase.OrderBy(a => a.Id).Last();
                    context.SaveChanges();
                }
                else
                {
                    child = context.ChildDatabase.Where(a => a.Id == parent.Child.Id).FirstOrDefault();
                    child.GrandChild = grandchild;
                    context.GrandChildDatabase.Update(grandchild);
                    context.SaveChanges();
                }
            }




                if (context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault() == null)
                {

                    parent.Id = 0;
                    parent.Child = child;
                    context.ParentDatabase.Add(parent);
                    context.SaveChanges();
                }
                else
                {
                    var retrievedparent = context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault();
                    retrievedparent.Child = child;
                    context.ParentDatabase.Update(parent);
                    context.SaveChanges();
                }
            
            


        }

        public static void DeleteParent(TestDatabase context, Parent parent)
        {
            context.ChangeTracker.Clear();
            if (context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault() != null)
            {
                context.ParentDatabase.Remove(parent);
                context.SaveChanges();
            }
        }

        public static void DeleteChild(TestDatabase context, Child child)
        {
            context.ChangeTracker.Clear();
            if (context.ChildDatabase.Where(a => a.Id == child.Id).FirstOrDefault() != null)
            {
                context.ChildDatabase.Remove(child);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// all parent nodes containing this will automaticly update
        /// </summary>

        public static void UpdateGrandchild(TestDatabase context,GrandChild grandchild)
        {
            context.GrandChildDatabase.Update(grandchild);
            context.SaveChanges();
        }

        public static void UpdateParent(TestDatabase context, Parent parent)
        {
            if (context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault() != null)
            {
                context.Update(parent);
                context.SaveChanges();
            }
        }

        public static Parent GetParent(int id,TestDatabase context)
        {
            if (context.ParentDatabase.Where(a => a.Id == id).FirstOrDefault() != null)
            {
                var parent = context.ParentDatabase.Where(a => a.Id == id).FirstOrDefault();
                if (context.ChildDatabase.Where(a => a.Id == parent.ChildId).FirstOrDefault() != null)
                {
                    parent.Child = context.ChildDatabase.Where(a => a.Id == parent.ChildId).FirstOrDefault();
                    if (context.GrandChildDatabase.Where(a => a.Id == parent.Child.GrandChildId).FirstOrDefault() != null)
                    {
                        parent.Child.GrandChild = context.GrandChildDatabase.Where(a => a.Id == parent.Child.GrandChildId).FirstOrDefault();
                    }
                    else
                    {
                        parent.Child.GrandChild = null; 
                    }
                }
                else
                {
                    parent.Child = null;
                }
                return parent;
            }
            else
            {
                return null;
            }
        }
        public static void Clear(TestDatabase context)
        {
            context.Database.ExecuteSqlRaw("DELETE FROM [ChildDatabase]");
            context.Database.ExecuteSqlRaw("DELETE FROM [GrandChildDatabase]");
            context.Database.ExecuteSqlRaw("DELETE FROM [ParentDatabase]");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('ChildDatabase',RESEED,0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('GrandChildDatabase',RESEED,0);");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('GrandChildDatabase',RESEED,0);");

        }
        static void Main(string[] args)
        {
            TestDatabase context = new TestDatabase();
            var parent = new Parent()
            {
                Text = "hello from parent",
                Child = new Child()
                {
                    Id = 1,
                    Text = "hello from child",
                    GrandChild = new GrandChild()
                    {
                        Id = 2,
                        Text = "hello from grandchild"
                    }
                },
                GrandChildren = new System.Collections.Generic.List<GrandChild>()
                { 
                    new GrandChild()
                    { 
                      Id = 1,
                      Text = "Grandchild1"
                    },
                    new GrandChild()
                    { 
                      Id = 2,
                      Text = "Grandchild2"
                    }
                }
            };

            var listparent = new Parent()
            {
                Text = "hello from parent",
                GrandChildren = new System.Collections.Generic.List<GrandChild>()
                { 
                  new GrandChild()
                  {
                       Id = 1,
                       Text = "Grandchild1"
                  },
                  new GrandChild()
                  { 
                     Id = 2,
                     Text = "Grandchild2"
                  }
                }
            };
            //Add(parent, context);
            Add(parent,context);
            Console.WriteLine("");
        }
    }
}
