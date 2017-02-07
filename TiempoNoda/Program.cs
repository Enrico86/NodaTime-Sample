using System;
using NodaTime;

namespace TiempoNoda
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			// https://www.youtube.com/watch?v=-5wpm-gesOY

			Console.WriteLine("Tradicional:");
			var ahora = DateTime.UtcNow; // UTC
			var postPublication = new DateTime(2017, 2, 7, 23, 0, 0);
			Console.WriteLine("UTC:\t\t" + ahora.ToString("o"));
			var ahoraLocal = ahora.ToLocalTime(); // Hora local
			Console.WriteLine("Hora local:\t" + ahoraLocal.ToString("o"));

			// Normalmente la Ciudad de México está en -6... ¿o -5?
			DateTime mexicoAhora = postPublication.AddHours(-6);
			Console.WriteLine("Mexico City:\t" + mexicoAhora.ToString("o"));

			// Normalmente la Copenhagen está en +1... ¿o +2?
			DateTime copenhagenAhora = postPublication.AddHours(1);
			Console.WriteLine("Copenhagen:\t" + copenhagenAhora.ToString("o"));


			Console.WriteLine("NodaTime:");
			Instant now = SystemClock.Instance.Now; // UTC
			var publicacionPost = Instant.FromUtc(2017, 2, 7, 23, 0, 0); // UTC
			Console.WriteLine("UTC:\t\t" + now);


			// Obtenemos la zona horaria local del proveedor Bcl (sistema)
			DateTimeZone localTimeZone = DateTimeZoneProviders.Bcl.GetSystemDefault();
			// Conversión a la zona horaria local
			ZonedDateTime localNow = now.InZone(localTimeZone);
			Console.WriteLine("Hora local:\t" + localNow);

			DateTimeZone mexicoTimeZone = DateTimeZoneProviders.Tzdb["America/Mexico_City"];
			ZonedDateTime mexicoNow = publicacionPost.InZone(mexicoTimeZone);
			Console.WriteLine("Mexico City:\t" + mexicoNow);

			DateTimeZone copenhagenTimeZone = DateTimeZoneProviders.Tzdb["Europe/Copenhagen"];
			ZonedDateTime copenhagenNow = publicacionPost.InZone(copenhagenTimeZone);
			Console.WriteLine("Copenhagen:\t" + copenhagenNow);

			var partidoInicialCopa = new DateTime(2017, 6, 17); 
			var cupInitialMatch = new LocalDate(2017, 6, 17);

			var finalCopa = new DateTime(2017, 7, 2);
			var cupFinal = new LocalDate(2017, 7, 2);

			var duracionCopa = finalCopa - partidoInicialCopa;
			var cupDuration = Period.Between(cupInitialMatch, cupFinal).ToDuration();

			Console.WriteLine(duracionCopa);
			Console.WriteLine(cupDuration);

			var reminding = finalCopa - ahoraLocal.Date;
			Console.WriteLine("Restante: " + reminding);


			var restante = Period.Between(localNow.Date, cupFinal);
			Console.WriteLine("Restante: " + restante);


			var in3Months = ahoraLocal.AddMonths(3);
			var en3Meses = localNow.Date.PlusMonths(3);
			Console.WriteLine(String.Format("{0:d}", in3Months));
			Console.WriteLine(String.Format("{0:d}",en3Meses));


		}
	}
}
