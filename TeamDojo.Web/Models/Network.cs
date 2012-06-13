using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TeamDojo.Web.Models
{
	public class Network
	{
		private const decimal DAMPENING_FACTOR = 0.85m;

		public static Network Load(string xml)
		{
			var ser = new XmlSerializer(typeof(Network));
			return (Network)ser.Deserialize(XmlReader.Create(xml));
		}

		[XmlElement("Programmer")]
		public List<Programmer> Programmers { get; set; }


		private void SetDefaultKudos()
		{
			foreach (var programmer in Programmers)
			{
				programmer.Kudos = 0;
			}
		}

		public void PopulateKudos()
		{
			SetDefaultKudos();

			decimal averagePr;

			do
			{
				var result = Programmers.Select(x => x.Name + ":" + x.Kudos);
				Debug.WriteLine(string.Join(" " , result));
				UpdateKudos();

				averagePr = Programmers.Sum(x => x.Kudos) / Programmers.Count;

				Debug.WriteLine(" >>> " + averagePr);

			} while (averagePr < 0.9999999999m);
		}

		private void UpdateKudos()
		{
			foreach (var programmer in Programmers)
			{
				programmer.Kudos =CalulateKudos(programmer);
			}
		}

		private decimal CalulateKudos(Programmer programmer)
		{
			decimal weight = 0;

			foreach (var recommender in GetMyRecomendations(programmer).ToList())
			{
				var reccomendersKudos = GetReccomendersKudos(recommender);
				var reccomendersLinks = GetReccomendersLinks(recommender);
				weight = weight + (DAMPENING_FACTOR * reccomendersKudos / reccomendersLinks);
			}

			return 1 - DAMPENING_FACTOR + weight;
		}

		private decimal GetReccomendersLinks(string recommender)
		{
			var programmer = Programmers.FirstOrDefault(x => x.Name == recommender);
			return programmer == null ? 0 : programmer.Recommendations.Count;
		}

		private decimal GetReccomendersKudos(string recommender)
		{
			var programmer = Programmers.FirstOrDefault(x => x.Name == recommender);
			return programmer == null ? 0 : programmer.Kudos;
		}

		private IEnumerable<string> GetMyRecomendations(Programmer programmer)
		{
			foreach (var inbound in Programmers)
			{
				if (inbound.Recommendations.Any(x => x.Name == programmer.Name))
				{
					yield return inbound.Name;
				}
			}
		}
	}
}