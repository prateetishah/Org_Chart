using System;
using System.Collections.Generic;

namespace Org_Chart
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrgChart org = new OrgChart();
            org.add(10, "Sharilyn Gruber", -1);
            org.add(7, "Denice Mattice", 10);
            org.add(3, "Lawana Futrell", -1);
            org.add(34, "Lissette Gorney", 7);
            org.add(5, "Lan Puls", 3);
            org.print();
            Console.WriteLine("------------------");
            org.move(5, 10);
            org.print();
            Console.WriteLine("------------------");
            Console.WriteLine(org.count(10));
        }

        public class OrgChart
        {
            Dictionary<int, Employee> dict = new Dictionary<int, Employee>();

            public void add(int id, string name, int mId)
            {
                Employee emp = new Employee(id, name, mId);
                dict.Add(id, emp);
            }

            public void remove(int id)
            {
                dict.Remove(id);
            }

            public void move(int id, int mId)
            {
                dict.GetValueOrDefault(id).managerId = mId;
            }

            public int count(int mId)
            {
                if (dict.ContainsKey(mId)){
                    Dictionary<int, List<Employee>> m = new Dictionary<int, List<Employee>>();
                    foreach(Employee emp in dict.Values)
                    {
                        if (m.ContainsKey(emp.managerId))
                        {
                            m.GetValueOrDefault(emp.managerId).Add(emp);
                        }
                        else
                        {
                            m.Add(emp.managerId, new List<Employee>());
                        }
                    }

                    return dfs(m, mId);
                }

                return -1;
            }

            private int dfs(Dictionary<int,List<Employee>> m, int mId, List<int> counted = null)
            {
                int count = 0;

                foreach(Employee emp in m.GetValueOrDefault(mId))
                {
                    if (!counted.Contains(emp.empId))
                    {
                        counted.Add(emp.empId);
                        count += (1 + dfs(m,emp.empId,counted));
                    }
                }

                return count;
            }

            public void print()
            {
                
                Dictionary<int, List<Employee>> m = new Dictionary<int, List<Employee>>();
                foreach (Employee emp in dict.Values)
                {
                    if (m.ContainsKey(emp.managerId))
                    {
                        m.GetValueOrDefault(emp.managerId).Add(emp);
                    }
                    else
                    {
                        m.Add(emp.managerId, new List<Employee>());
                    }
                }
                dfsprint(m, -1);
            }

            private void dfsprint(Dictionary<int,List<Employee>> m,int mId,string s = "",List<int> counted = null)
            {
                foreach(Employee emp in m.GetValueOrDefault(mId))
                {
                    if (!counted.Contains(emp.empId))
                    {
                        Console.WriteLine(s + emp);
                        counted.Add(emp.empId);
                        dfsprint(m, emp.empId, "  ", counted);
                    }
                }
            }
        }

        public class Employee
        {
            public int empId;
            public string empName;
            public int managerId;

            public Employee(int id, string name, int mId)
            {
                this.empId = id;
                this.empName = name;
                this.managerId = mId;
            }
        }
    }
}
