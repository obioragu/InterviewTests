using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class GraduationTrackerTests
    {
        [TestMethod]
        //Confirm that graduated students have required credits
        public void TestHasGraduated()
        {
            var tracker = new GraduationTracker();
            //get dummy data objects for testing
            var diploma = DummyRepository.GetDummyDiploma();
            var students = DummyRepository.GetDummyStudents();


            //confirm that students have graduated as expected
            var graduated = new List<Tuple<bool, STANDING>>();
            foreach (var student in students)
            {
                graduated.Add(tracker.HasGraduated(diploma, student));
            }

            //initialize expected result
            var expected = new List<Tuple<bool, STANDING>>
            {
                Tuple.Create( true, STANDING.SumaCumLaude ),
                Tuple.Create( true, STANDING.MagnaCumLaude),
                Tuple.Create( true, STANDING.Average ),
                Tuple.Create(false, STANDING.Remedial)
               };

            //compare expected with method result
            Assert.IsTrue(graduated.Count == expected.Count && !graduated.Except(expected).Any());

        }


    }
}
