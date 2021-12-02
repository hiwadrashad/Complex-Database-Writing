using Database_Test.Database;
using Database_Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database_Test
{
    class Program
    {
        public static void Add(Parent parent, TestDatabase context)
        {
            GrandChild grandchild = new GrandChild();
            Child child = new Child();
            List<int> GrandchildrenFK = new List<int>();
            if (parent.GrandChildren == null)
            {
                parent.GrandChildren = new List<GrandChild>();
            }
            foreach (var item in parent.GrandChildren)
            {
                if (context.GrandChildDatabase.Where(a => a.Id == item.Id).FirstOrDefault() == null)
                {
                    item.Id = 0;
                    context.GrandChildDatabase.Add(item);
                    context.SaveChanges();
                    GrandchildrenFK.Add(item.Id);
                }
                else
                {
                    context.ChangeTracker.Clear();
                    GrandchildrenFK.Add(item.Id);
                    context.GrandChildDatabase.Update(item);
                    context.SaveChanges();
                }
                parent.GrandChildren = new List<GrandChild>();
            }
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
                        context.GrandChildDatabase.Update(grandchild);
                        context.SaveChanges();
                    }
                    parent.Child.GrandChild = null;
                }

                if (context.ChildDatabase.Where(a => a.Id == parent.Child.Id).FirstOrDefault() == null)
                {
                    parent.Child.Id = 0;
                    parent.Child.GrandChildId = grandchild.Id;
                    context.ChildDatabase.Add(parent.Child);
                    context.SaveChanges();
                    child = context.ChildDatabase.OrderBy(a => a.Id).Last();
                    context.SaveChanges();
                }
                else
                {
                    child = context.ChildDatabase.Where(a => a.Id == parent.Child.Id).FirstOrDefault();
                    child.GrandChildId = grandchild.Id;
                    context.ChildDatabase.Update(child);
                    context.SaveChanges();
                }
                parent.Child = null;
            }




            if (context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault() == null)
            {

                parent.Id = 0;
                parent.ChildId = child.Id;
                parent.GrandChildrenId = Converter.ListConverter.ListToString(GrandchildrenFK);
                context.ParentDatabase.Add(parent);
                context.SaveChanges();
            }
            else
            {
                var retrievedparent = context.ParentDatabase.Where(a => a.Id == parent.Id).FirstOrDefault();
                retrievedparent.Child = child;
                retrievedparent.ChildId = child.Id;
                retrievedparent.GrandChildrenId = Converter.ListConverter.ListToString(GrandchildrenFK);
                context.ParentDatabase.Update(retrievedparent);
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
                context.ChangeTracker.Clear();
                context.Update(parent);
                context.SaveChanges();
            }
            if (parent.Child != null)
            {
                context.Update(parent.Child);
                context.SaveChanges();
            }
            if (parent.Child.GrandChild != null)
            {
                context.Update(parent.Child.GrandChild);
                context.SaveChanges();
            }
            if (parent.GrandChildren != null)
            {
                if (parent.GrandChildren.Count > 0)
                {
                    List<int> GrandChildrenFK = new List<int>();
                    foreach (var item in parent.GrandChildren)
                    {
                        GrandChildrenFK.Add(item.Id);
                        context.Update(item);
                        context.SaveChanges();
                    }
                    parent.GrandChildrenId = Converter.ListConverter.ListToString(GrandChildrenFK);
                    context.Update(parent);
                    context.SaveChanges();
                }
            }
        }

        public static Parent GetParent(int id,TestDatabase context)
        {
            if (context.ParentDatabase.Where(a => a.Id == id).FirstOrDefault() != null)
            {
                var parent = context.ParentDatabase.Where(a => a.Id == id).FirstOrDefault();
                List<GrandChild> grandchildren = new List<GrandChild>();
                List<int> GrandChildrenIds = Converter.ListConverter.StringToList(parent.GrandChildrenId);
                if (GrandChildrenIds != null)
                {
                    if (GrandChildrenIds.Count > 0)
                    {
                        foreach (var item in GrandChildrenIds)
                        {
                            if (context.GrandChildDatabase.Where(a => a.Id == item).FirstOrDefault() != null)
                            {
                                grandchildren.Add(context.GrandChildDatabase.Where(a => a.Id == item).FirstOrDefault());
                            }
                        }
                        parent.GrandChildren = grandchildren;
                    }
                    else
                    {
                        parent.GrandChildren = new List<GrandChild>();
                    }
                }
                else
                {
                    parent.GrandChildren = new List<GrandChild>();
                }
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
                        Id = 3,
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

            var parenttoupdate = new Parent()
            {
                Id = 2,
                Text = "hello from parent updated",
                Child = new Child()
                {
                    Id = 1,
                    Text = "hello from child updated",
                    GrandChild = new GrandChild()
                    {
                        Id = 3,
                        Text = "hello from grandchild updated"
                    }
                },
                GrandChildren = new System.Collections.Generic.List<GrandChild>()
                {
                    new GrandChild()
                    {
                      Id = 1,
                      Text = "Grandchild1 updated"
                    },
                    new GrandChild()
                    {
                      Id = 2,
                      Text = "Grandchild2 updated"
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

            var intlist = new List<int>()
            {
                1,
                2
            };
            //Add(parent, context);
            UpdateParent(context, parenttoupdate);
            Parent item = GetParent(2, context);
            Console.WriteLine(item.Child.Text);
            Console.WriteLine(item.Child.GrandChild.Text);
            Console.WriteLine(item.GrandChildren.FirstOrDefault().Text);
        }
    }
}
