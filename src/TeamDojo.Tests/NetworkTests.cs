using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TeamDojo.Web.Models;

namespace TeamDojo.Tests
{
	[TestFixture]
	public class when_parsing_a_network
	{
		[Test]
		public void then_the_file_should_load()
		{
			var network = Network.Load("pronet.xml");
			Assert.That(network.Programmers.Count, Is.EqualTo(10));
			var programmer = network.Programmers.First();
			Assert.That(programmer.Name, Is.EqualTo("Bill"));

			Assert.That(programmer.Recommendations.Count, Is.EqualTo(4));
			Assert.That(programmer.Recommendations.First().Name, Is.EqualTo("Jason"));

			Assert.That(programmer.Skills.Count, Is.EqualTo(3));
			Assert.That(programmer.Skills.First().Value, Is.EqualTo("Ruby"));
		}
	}

	[TestFixture]
	public class with_a_simple_binary_network
	{
		private Network _network = new Network()
		{
			Programmers = new List<Programmer>
		    {
		        new Programmer
		            {
		                Name = "A",
		                Recommendations = new List<Recommendation>
		                          		    {
		                          		        new Recommendation {Name = "B"}
		                          		    }
		            },
		        new Programmer
		            {
		                Name = "B",
		                Recommendations = new List<Recommendation>
		                          		    {
		                          		        new Recommendation {Name = "A"}
		                          		    }
		            }
		    }
		};

		[Test]
		public void everyones_kudos_should_be_1()
		{
			_network.PopulateKudos();

			foreach (var programmer in _network.Programmers)
			{
				Assert.That(Decimal.Round(programmer.Kudos, 2), Is.EqualTo(1));
			}
		}
	}

	[TestFixture]
	public class with_a_complex_network
	{
		private Network _network = new Network()
		{
			Programmers = new List<Programmer>
		    {
		        new Programmer
		            {
		                Name = "A",
		                Recommendations = new List<Recommendation>
		                           		    {
												new Recommendation {Name = "B"},
		                           		        new Recommendation {Name = "C"},
		                           		    }
		            },
		        new Programmer
		            {
		                Name = "B",
		                Recommendations = new List<Recommendation>
		                           		    {	
		                           		        new Recommendation {Name = "C"},
		                           		    }
		            },
		        new Programmer
		            {
		                Name = "C",
		                Recommendations = new List<Recommendation>
		                           		    {
		                           		        new Recommendation {Name = "A"},
		                           		    }
		            },
		        new Programmer
		            {
		                Name = "D",
		                Recommendations = new List<Recommendation>
		                           		    {
		                           		        new Recommendation {Name = "C"}
		                           		    }
		            }
		    }
		};

		[Test]
		public void kudos_should_be_calculated()
		{
			_network.PopulateKudos();

			Assert.That(Decimal.Round(_network.Programmers[0].Kudos, 2), Is.EqualTo(1.49));
			Assert.That(Decimal.Round(_network.Programmers[1].Kudos, 2), Is.EqualTo(0.78));
			Assert.That(Decimal.Round(_network.Programmers[2].Kudos, 2), Is.EqualTo(1.58));
			Assert.That(Decimal.Round(_network.Programmers[3].Kudos, 2), Is.EqualTo(0.15));

		}
	}
}
