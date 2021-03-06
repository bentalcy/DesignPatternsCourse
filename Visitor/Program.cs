﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    /// <summary>
    /// Element
    /// </summary>
    class Employee
    {
        public Employee(string name, int salary, int seniority)
        {
            Name = name;
            BaseSalary = salary;
            Seniority = seniority;
        }

        public string Name { get; set; }
        public int BaseSalary { get; set; }
        public int Seniority { get; set; }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public virtual int GetSalary(SalaryVisitor visitor)
        {
            return visitor.VisitSalary(this);
        }
    }

    /// <summary>
    /// ConcreteElement
    /// </summary>
    class Developer : Employee
    {
        public Developer(string name, int salary, int seniority)
            : base(name, salary, seniority)
        { }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public override int GetSalary(SalaryVisitor visitor)
        {
            return visitor.VisitSalary(this);
        }
    }

    /// <summary>
    /// ConcreteElement
    /// </summary>
    class Ceo : Employee
    {
        public Ceo(string name, int salary, int seniority)
            : base(name, salary, seniority)
        { }

        /// <summary>
        /// Accept method
        /// </summary>
        /// <param name="visitor"></param>
        /// <returns></returns>
        public override int GetSalary(SalaryVisitor visitor)
        {
            return visitor.VisitSalary(this);
        }
    }

    /// <summary>
    /// Visitor
    /// </summary>
    class SalaryVisitor
    {
        public virtual int VisitSalary(Employee e)
        {
            return e.BaseSalary;
        }

        public virtual int VisitSalary(Developer dev)
        {
            // 10% raise for each year serving the company
            return dev.BaseSalary + 100 * dev.Seniority;
        }

        public virtual int VisitSalary(Ceo ceo)
        {
            // 10% raise for each year serving the company
            return ceo.BaseSalary + (ceo.BaseSalary * ceo.Seniority / 10);
        }
    }

    /// <summary>
    /// ConcreteVisitor
    /// </summary>
    class BonusSalaryVisitor : SalaryVisitor
    {
        public override int VisitSalary(Employee e)
        {
            return e.BaseSalary + 100;
        }

        public override int VisitSalary(Ceo e)
        {
            // 10% raise for each year serving the company
            return base.VisitSalary(e) * 2;
        }
    }


    class Program
    {
        static void ProcessSalaries(IEnumerable<Employee> employees, SalaryVisitor salaryCalculator)
        {
            Console.WriteLine("{0,-12}{1,7}", "Employee", "Salary");
            foreach (var employee in employees)
            {
                Console.WriteLine("{0,-12}{1,7}", employee.Name, employee.GetSalary(salaryCalculator));
            }
        }

        static void Main(string[] args)
        {
            var employees = new List<Employee>
            {
                new Employee("Yosefa", 5000, 2),
                new Developer("Yossi", 20000, 5),
                new Ceo("Yosef", 100000, 3)
            };

            var currentMonth = DateTime.Now.Month;

            if (currentMonth != 12)
            {
                // normal month
                ProcessSalaries(employees, new SalaryVisitor());
            }
            else
            {
                // last month of the year - bonus time!
                ProcessSalaries(employees, new BonusSalaryVisitor());
            }
        }
    }
}
