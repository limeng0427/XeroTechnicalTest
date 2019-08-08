/*
    Welcome to the Xero technical excercise!
    ---------------------------------------------------------------------------------
    The test consists of a small invoice application that has a number of issues.

    Your job is to fix them and make sure you can perform the functions in each method below.

    Note your first job is to get the solution compiling! 
    
    Rules
    ---------------------------------------------------------------------------------
    * The entire solution must be written in C# (any version)
    * You can modify any of the code in this solution, split out classes, add projects etc
    * You can modify Invoice and InvoiceLine, rename and add methods, change property types (hint) 
    * Feel free to use any libraries or frameworks you like as long as they are .net based
    * Feel free to write tests (hint) 
    * Show off your skills! 

    Good luck :) 

    When you have finished the solution please zip it up and email it back to the recruiter or developer who sent it to you
*/

/*
 * Task Summary
 * 1. Fix compile issues: includes wrong spelling, data type mismatching.
 * 2. Rearrange solution folder structure. Add Unit Test and Libary projects. 
 * 3. Implement unfinished functions.
 * 4. Add Autofaq to realize ioc.
 * 5. Add Unit Test using xUnit. 17 unit test cases added.
 * 6. Add Log Utility. Add ability to switch to different logging mode.
 * 7. Remove Unnecessary Reference and Library.
 */


using System;
using Autofac;
using XeroTechnicalTest.BusinessServices;
using XeroTechnicalTest.Interfaces;
using XeroTechnicalTestLibrary;


namespace XeroTechnicalTest
{
    public class Program
    {
        // autofaq: ioc tool
        private static IContainer _container { get; set; }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Xero Tech Test!");

            StartService();

            Console.ReadLine();
        }

        static private void StartService()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            _container = builder.Build();
            using (ILifetimeScope scope = _container.BeginLifetimeScope())
            {
                //get dependency object
                var invoiceService = scope.Resolve<IInvoiceService>();
                var logger = new LogManager();

                logger.Log("1. Start Task: CreateInvoiceWithOneItem");
                invoiceService.CreateInvoiceWithOneItem();

                logger.Log("2. Start Task: CreateInvoiceWithMultipleItemsAndQuantities");
                invoiceService.CreateInvoiceWithMultipleItemsAndQuantities();

                logger.Log("3. Start Task: RemoveItem");
                invoiceService.RemoveItem();

                logger.Log("4. Start Task: MergeInvoices");
                invoiceService.MergeInvoices();

                logger.Log("5. Start Task: CloneInvoice");
                invoiceService.CloneInvoice();

                logger.Log("6. Start Task: InvoiceToString");
                invoiceService.InvoiceToString();

            }
        }

    }
}
