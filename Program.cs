using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LINQPractice
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
        public int StandardID { get; set; }
    }
    public class StudentComparer : IEqualityComparer<Student>
    {
        // override object.Equals
        public bool Equals(Student s1, Student s2)
        {
            if (s1 == s2 || s1.StudentID == s2.StudentID)
            {
                return true;
            }

            return false;    
        }

        // override object.GetHashCode
        public int GetHashCode(Student obj)
        {
            // TODO: write your implementation of GetHashCode() here
            return obj.GetHashCode();
        }
    }
    public class Standard
    {
        public int StandardID { get; set; }
        public string StandardName { get; set; }
    }
    public static class OwnMethods
    {
        public static IEnumerable<T> Jithe<T>(this IEnumerable<T> ptr, Func<T, bool> predicate)
        {
            List<T> list = new List<T>();
            foreach(var x in ptr)
            {
                if (predicate(x))
                    list.Add(x);
            }
            return list;
        }
    }
    class Program
    {
        public static IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", Age = 18 } ,
                new Student() { StudentID = 2, StudentName = "Steve",  Age = 15 } ,
                new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 } ,
                new Student() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                new Student() { StudentID = 5, StudentName = "Ron" , Age = 19 },
                new Student() { StudentID = 6, StudentName = "Ram" , Age = 18 }
            };
        public static IList<Student> studentList1 = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "John", StandardID =1 },
                new Student() { StudentID = 2, StudentName = "Moin", StandardID =2 },
                new Student() { StudentID = 3, StudentName = "Bill", StandardID =2 },
                new Student() { StudentID = 4, StudentName = "Ram" , StandardID =2 },
                new Student() { StudentID = 5, StudentName = "Ron"  }
            };

        public static IList<Standard> standardList = new List<Standard>() {
                new Standard(){ StandardID = 1, StandardName="Standard 1"},
                new Standard(){ StandardID = 1, StandardName="Standard 2"},
                new Standard(){ StandardID = 3, StandardName="Standard 3"}
            };
        public static IList<string> list1 = new List<string>()
            {
                "One",
                "Two",
                "Thr",
                "Four"
            };
        public static IList<string> list2 = new List<string>()
            {
                "One",
                "Two",
                "Five",
                "Six"
            };

        public static List<int> listInts = new List<int>()
        {
            1,2,3,5,6,7
        };
        static void Main(string[] args)
        {
            LinqJithe();
            Console.WriteLine("............Break................");
            LinqContains();
            Console.WriteLine("............Break................");
            LinqOfType();
            Console.WriteLine("............Break................");
            LinqOrderBy();
            LinqOrderByandThen();
            Console.WriteLine("............Break................");
            Console.WriteLine("LINQGroupby()");
            LINQGroupby();
            Console.WriteLine("............Break................");
            Console.WriteLine("LINQJoin()");
            LINQJoin();
            Console.WriteLine("............Break................");
            Console.WriteLine("LINQGroupJoin()");
            LINQGroupJoin();
            Console.WriteLine("............Break................");
            Console.WriteLine("LINQAllAny()");
            LINQAllAny();
            Console.WriteLine("............Break................");
            Console.WriteLine("............Break................");
            LINQContainsWithComparer();
            Console.WriteLine("............Break................");
            LINQAggregrate();
            Console.WriteLine("............Break................");
            LINQCountWordsString();
            Console.WriteLine("............Break................");
            LINQSum();
            Console.WriteLine("............Break................");
            LINQSqrNo();
            Console.WriteLine("............Break................");
            LINQExcept();
            Console.WriteLine("............Break................");

            LINQSkip();
            Console.WriteLine("............Break................");
            LINQExpressionTree();




            Console.ReadKey();

        }
        private static void LINQExpressionTree()
        {
            ParameterExpression pe = Expression.Parameter(typeof(Student), "student");
            MemberExpression me = Expression.Property(pe, "Age");
            ConstantExpression ce = Expression.Constant(18, typeof(int));
            BinaryExpression body = Expression.GreaterThanOrEqual(me, ce);

            var isAdult = Expression.Lambda<Func<Student, bool>>(body, new[] { pe });

            Console.WriteLine("Expression tree : {0}", isAdult);
            Console.WriteLine("Expression Body: {0}", isAdult.Body);
            Console.WriteLine("Number of params : {0}", isAdult.Parameters.Count);
            Console.WriteLine("params in Expression : {0}", isAdult.Parameters[0]);
            Console.WriteLine("Left : {0}, Right: {1}", body.Left, body.Right);

        }
        private static void LINQSkip()
        {
            var res = listInts.SkipWhile(s => s != 3).TakeWhile(s => s != 7);

            foreach(var r in res)
                Console.WriteLine(r);
        }
        private static void LINQExcept()
        {
            var res = list1.Except(list2);
            res = list1.Union(list2);
            res = list1.Intersect(list2);

            foreach (var r in res)
                Console.WriteLine(r);
        }
        private static void LINQSqrNo()
        {
            var res = from x in listInts
                      group x by x into y

                      select y;

            //foreach(var r in res)
            //    Console.WriteLine(r.x +" "+ r.y);
        }
        private static void LINQSum()
        {
            var res = studentList.Sum(i => { if (i.Age >= 18)
                    return 1;
                return 0;
                    });
            Console.WriteLine(res);
        }
        private static void LINQCountWordsString()
        {
            string str = "I am an human";
            var res = str.Split(' ');
            Console.WriteLine(res.Count());
                      
        }
        private static void LINQAggregrate()
        {
            var res = studentList.Aggregate<Student, string, int>("Seed: ", (s1, s2) => s1 += s2.StudentName + ", ", s1 => s1.Length);
            Console.WriteLine(res);
        }
        private static void LINQContainsWithComparer()
        {
            Student st = new Student() { StudentID = 16, StudentName = "Johen", Age = 18 };
            var res = studentList.Contains(st, new StudentComparer());
            Console.WriteLine(res);
        }
        private static void LINQAllAny()
        {
            bool res = list1.All(s => s == list1.Last());
            res = list1.Any(s => s == list1.First());

            Console.WriteLine(res);
        }
        private static void LINQGroupJoin()
        {
            var res = list1.GroupJoin(list2,
                s1 => s1,
                s2 => s2,
                (s1, s2) => s1);

            var result = standardList.GroupJoin(studentList1,
                s => s.StandardID,
                st => st.StandardID,
                (s, st) => new
                {
                    st,
                    s.StandardName
                }
                );

            //Query syntax for Group Join

            var queryResult = from s in standardList
                              join st in studentList1
                              on s.StandardID equals st.StandardID
                                into groupedCol
                              select new
                              {
                                  st = groupedCol,
                                  s = s.StandardName
                              };


            foreach(var s in queryResult)
            {
                Console.WriteLine(s.s);
                foreach(var x in s.st)
                {
                    Console.WriteLine(x.StudentName);
                }
            }
            Console.WriteLine("Letss see");
            foreach(var s in res)
                Console.WriteLine(s);

        }
        private static void LINQJoin()
        {
            var innerJoin = list1.Join(list2, s => s, s2 => s2,
                (s, s2) => s);
            foreach(string s in innerJoin)
                Console.WriteLine(s);
            Console.WriteLine("Hurraayy");

            var resultJoin = studentList1.Join(standardList,
                s => s.StandardID,
                st => st.StandardID,
                (s, st) => new
                {
                    s.StudentName,
                    st.StandardName
                });

            var usingQueryJoin = from s in studentList1
                                 join st in standardList
                                 on s.StandardID equals st.StandardID
                                 select new
                                 {
                                     s.StudentName,
                                     st.StandardName
                                 };
            foreach(var s in usingQueryJoin)
                Console.WriteLine(s.StandardName + " " + s.StudentName);
        }

        private static void LINQGroupby()
        {
            studentList.Add(new Student { StudentID = 7, StudentName = "Nishant", Age = 25 });
            //var result = studentList.ToLookup(s => s.Age);
            var result = from s in studentList
                     group s by s.StudentName into y
                         //where y.Count() >1
                         select y;

            var res = studentList.GroupBy(s => s.StudentName)
                .Select(x => x.FirstOrDefault());   //Select distinct names

            foreach (var s in res)
            {
                Console.WriteLine(s.StudentID + " " + s.StudentName + " " + s.Age);
            }


            foreach (var x in result)
            {
                Console.WriteLine(x.Key);
                foreach (var s in x)
                    Console.WriteLine(s.StudentID + " " + s.StudentName + " " + s.Age);
            }

        }
        private static void LinqOrderByandThen()
        {
            var result = studentList.OrderByDescending(s => s.StudentName).ThenByDescending(s => s.Age);

            foreach (var s in result)
                Console.WriteLine(s.StudentID +" "+ s.StudentName+ " "+s.Age );

            Console.WriteLine("............Break................");
        }
        private static void LinqOrderBy()
        {
            List<int> age = new List<int>();
            age.Add(22);
            age.Add(35);
            age.Add(26);
            age.Add(15);
            age.Add(28);

            var res = from x in age
                  where x > 20
                  where x < 30
                  orderby x, x 
                  
                  select x;
            foreach (var s in res)
                Console.WriteLine(s);

            Console.WriteLine("............Break................");

        }
        private static void LinqOfType()
        {
            IList list = new ArrayList()
            {
                1,
                2,
                "Box",
                4.5f,
                354.5f,
                3
            };

            var res = from x in list.OfType<float>() 
                      where x > 4f
                      select x;

            foreach(var s in res)
                Console.WriteLine(s);
                      
        }
        private static void LinqContains()
        {
            IList<string> list = new List<string>()
            {
                "C# tutorials",
                "VB tutorials",
                "Learn C++",
                "Java"
            };

            var result = from x in list
                         where x.Contains("tutorials")
                         select x;

            foreach (var r in result)
                Console.WriteLine(r);
        }
        private static void LinqJithe()
        {
            List<int> age = new List<int>();
            age.Add(22);
            age.Add(35);
            age.Add(26);
            age.Add(15);
            age.Add(28);

            var res = age.Jithe(x => x > 20 && x < 60);
            var res1 = age.Where(x => x > 20 && x < 30);

            res = from x in age
                  where x < 30 
                  where x > 30
                  select x;
            foreach (var s in res)
                Console.WriteLine(s);
        }
    }
}
