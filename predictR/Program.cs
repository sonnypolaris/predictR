using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Diagnostics;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression;

namespace predictR
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var sr = new StreamReader(@"results.csv"))
            {
                var reader = new CsvReader(sr);

                //CSVReader will now read the whole file into an enumerable
                IEnumerable<MatchResult> records = reader.GetRecords<MatchResult>();

                //First 5 records in CSV file will be printed to the Output Window
                List<MatchResult> theResults = records.Take(1000).ToList();

                int i = 0;
                double[][] inputs = new double[theResults.Count][];
                double[] output = new double[theResults.Count];

                foreach (MatchResult record in theResults)
                {
                    inputs[i] = new double[2];
                    inputs[i][0] = record.Score;
                    inputs[i][1] = record.Spread;
                    output[i] = (record.isGood == true) ? 1 : 0;

                    Debug.Print("{2} {0} {1}", inputs[i][0], inputs[i][1], i);
                    i++;
                }

                // Create a Logistic Regression analysis
                var lra = new LogisticRegressionAnalysis()
                {
                    Regularization = 0
                };


                LogisticRegression regression = lra.Learn(inputs, output);
/*
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 7.5 , .35} ) );
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 10.6, .31 }));
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 9.697925, .55 }));
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 9.692093, .43 }));
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 7.5, .35 }));

                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 5.1900673, .02 }));
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 4.048, .26 }));

                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 5.689147, .04 }));
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 7.070967, .434 }));
*/
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 16.28828, .30 })); //D059061
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 4.3927097, .02 }));//F220746
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 7.0079713, .07 }));//E526357
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 5.7613797, .16 }));//G243328
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 14.592, .595 }));//j046558

                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 2.8236294, .302 }));//1868136
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 6.2766905, .5159 }));//1075341
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 2.17539, .404669 }));//1301361
                Debug.Print("{0:0.00}", lra.Regression.Score(new double[] { 6.006, .1235 }));//1125331 (true)


            }
        }
    }

    public class MatchResult
    {
        public bool isGood { get; set; }
        public string BuyId { get; set; }
        public double Score { get; set; }
        public double Score2 { get; set; }
        public double Spread { get; set;  }

    }
}
