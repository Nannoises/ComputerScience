using System;
using System.Collections.Generic;

namespace Organization
{
    public static class Company
    {
        private static Stack<Employee> currentPath; 
        private static Stack<Employee> firstEmployeePath;
        private static Stack<Employee> secondEmployeePath;
        
        public sealed class Employee
        {

            private readonly int id;
            private readonly string name;
            private List<Employee> reports;

            public Employee(int id, string name)
            {
                this.id = id;
                this.name = name;
                this.reports = new List<Employee>();
            }

            public int getId()
            {
                return id;
            }

            public string getName()
            {
                return name;
            }

            public IList<Employee> getReports()
            {
                return reports;
            }

            public void addReport(Employee employee)
            {
                reports.Add(employee);
            }
        }

        //This approach tracks the current path while traversing the employee structure. If one of the target employees 
        //is found, that path is saved. Once both employees are found, we can trace back to the first common manager.
        //Total time is O(n) as tree is traversed once, and then paths are traced back once.
        public static Employee closestCommonManager(Employee ceo, Employee firstEmployee, Employee secondEmployee)
        {
            currentPath = new Stack<Employee>();
            firstEmployeePath = null;
            secondEmployeePath = null;
            TraverseNode(ceo, firstEmployee, secondEmployee);

            //If either path is null, that employee was not found
            if(firstEmployeePath == null)
                throw new Exception("First employee not found!");

            if(secondEmployeePath == null)
                throw new Exception("Second employee not found!");

            //Prune longer path down to length of shorter. O(n)
            while (firstEmployeePath.Count != secondEmployeePath.Count)
            {
                var longerPath = firstEmployeePath.Count > secondEmployeePath.Count
                    ? firstEmployeePath
                    : secondEmployeePath;

                longerPath.Pop();
            }

            //Run up the paths until common manager is found. O(n)
            while (firstEmployeePath.Count > 0 && secondEmployeePath.Count > 0)
            {
                var currentFirstAncestor = firstEmployeePath.Pop();
                var currentSecondAncestor = secondEmployeePath.Pop();
                if (currentFirstAncestor.getId() == currentSecondAncestor.getId())
                    return currentFirstAncestor;
            }
            
            //No common manager was found. Shouldn't occur based on assumption that CEO is above all employees.
            throw new Exception("No common manager found!");
        }

        private static void CopyPath(Stack<Employee> source, Stack<Employee> dest)
        {            
            //Need to preserve path order so copy in reverse;
            var sourceArray = source.ToArray();
            for (var i = sourceArray.Length - 1; i >= 0; i--)
            {
                dest.Push(sourceArray[i]);
            }            
        }

        //DFS traversal of nodes. Improved by returning after both employees are found. Worst case O(n) since visiting each employee once.
        private static void TraverseNode(Employee root, Employee firstEmployee, Employee secondEmployee)
        {
            //Bottomed out. (Base case)
            if (root == null)
                return;

            currentPath.Push(root);
            
            if (firstEmployee.getId() == root.getId())
            {
                firstEmployeePath = new Stack<Employee>();
                CopyPath(currentPath, firstEmployeePath);
            } 
            else if (secondEmployee.getId() == root.getId())
            {
                secondEmployeePath = new Stack<Employee>();
                CopyPath(currentPath, secondEmployeePath);
            }

            foreach (var employee in root.getReports())
            {
                //If both found, terminate traversal
                if (firstEmployeePath != null && secondEmployeePath != null)
                    return;

                TraverseNode(employee, firstEmployee, secondEmployee);

                currentPath.Pop();
            }
        }        


        public static void RunTests()
        {
            var Bill = new Employee(0, "Bill");
            var Dom = new Employee(1, "Dom");
            var Samir = new Employee(2, "Samir");
            var Micheal = new Employee(3, "Micheal");
            var Peter = new Employee(4, "Peter");
            var Bob = new Employee(5, "Bob");
            var Porter = new Employee(6, "Porter");
            var Milton = new Employee(7, "Milton");
            var Nina = new Employee(8, "Nina");

            Bill.addReport(Dom);
            Bill.addReport(Samir);
            Bill.addReport(Micheal);

            Dom.addReport(Peter);
            Dom.addReport(Bob);
            Dom.addReport(Porter);

            Peter.addReport(Milton);
            Peter.addReport(Nina);

            Console.WriteLine("Milton, Nina = " + closestCommonManager(Bill, Milton, Nina).getName());
            Console.WriteLine("Nina, Porter = " + closestCommonManager(Bill, Nina, Porter).getName());
            Console.WriteLine("Nina, Samir = " + closestCommonManager(Bill, Nina, Samir).getName());
            Console.WriteLine("Peter, Nina = " + closestCommonManager(Bill, Peter, Nina).getName());
            Console.ReadLine();
        }
    }
}