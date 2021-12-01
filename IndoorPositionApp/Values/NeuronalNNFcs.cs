using System;
using System.Collections.Generic;


namespace IndoorPositionApp.Values
{


    class NeuronalNNF
    {
        double[] x = new double[4];

        // Input 1
        double[] x1_step1_xoffset = { 0.78, 0.35, 0.52, 0.4 };//Array Vertical
        double[] x1_step1_gain = { 0.120481927710843, 0.0057058085130663, 0.0996512207274539, 0.0719683339330694 }; //Array Vertical
        int x1_step1_ymin = -1;


        int[,] matriz = { { 1, 2, 3 }, { 1, 2, 3 } };
        // Layer 1
        double[] b1 = { -6.9028972064798708175, 165.10396870980255812, -101.2783146056947885, -215.30835093682151182, 12.374349481078395385, -62.618664063197151393, 186.54329186698620902, 39.898235056091401418, 16.216478770027794809, 19.404728774684720349 }; //array vertical
        double[,] IW1_1 = { { -2.9188427201328188687, 16.104637767348883415, 1.1479016603482525838, -23.859282439938308329 },
                            { 128.83686496105582364, 43.316255852198956688, 43.115994378310226409, -67.770190707602068869 },
                            { 6.6912525788190730935, 128.55240736257024992, -137.86533863672283928, -120.60676405737736161 },
                            { -3.816627166601062715, -360.08557619478818879, 105.11840891679516119, 48.519567618519658936 },
                            { 1.4430125529400739115, 16.607585863651056712, 0.76622949533658668564, -5.5167837462326838605 },
                            { -0.26973460702933321764, 13.04254735869986348, -22.520529454510157308, -58.146952533589590928 },
                            { -25.724079346622737319, 382.5718961729738794, -29.827591220905564029, -182.14611712087952355 },
                            { 1.3026887618554428006, -0.84342435625682399269, 18.03544275659606555, 24.865215151803301552 },
                            { 57.675713947239756862, -27.742484033288093315, 6.3250210049562562986, -22.462653270165340302 },
                            { 2.3860089160718471746, 10.378453613498280106, -11.96520155118882478, 23.245411538967747589 } };
        // Layer 2
        double[] b2 = { 0.13249738196131655155, -0.63257986571631230621, -0.073212313824536037754, 0.93710820735169664264 };//Array vertical
        double[,] LW2_1 = { { -0.2770346334374522157, -0.26279354063858489932, -0.022152771796345321204, 0.37063274774721199911, -0.040316469376886099329, 0.93739885885511098351, 0.61565825436765719836, 0.1719406490302327617, 0.70670745745154184281, 0.76414617972898757348 },
                            { 0.51771229510074412428, 0.18909844899307012844, 0.55736053867823354135, -0.23075848306354670991, -0.79881294778187228456, 1.4562849139284865796, -0.092104582194151449204, 1.9681330347446426288, 0.64304126115794646434, 0.37079427418361360358 },
                            { 0.47592171449795789284, 0.039083368909935724267, 0.41417747013973349457, -0.83043758466741057767, -0.85620785927865794562, 0.30047710677721889416, -0.64344682756983118121, 1.3989068564189388599, -0.64583729787054822147, -0.8733551803846417716 },
                            { -0.56720346068540794526, -0.16635211287726744955, -0.70038441129107553706, 0.17169767172732569582, 1.1973113552292065087, -1.8272688182718608285, -0.054154984840071931917, -2.3590851462304480712, -0.69666921535080883476, -0.5707240371129695955 } };
        // Output 1
        int y1_step1_ymin = -1;
        double[] y1_step1_gain = { 0.790513833992095, 0.766283524904215, 0.766283524904215, 0.790513833992095 };//Array vertical
        double[] y1_step1_xoffset = { 1.5, 1, 1, 1.5 };//Array vertical




        public NeuronalNNF(List<decimal> entrada)
        {
            double[] x1 = { decimal.ToDouble(entrada[0]), decimal.ToDouble(entrada[1]), decimal.ToDouble(entrada[2]), decimal.ToDouble(entrada[3]) }; //Array horizontal
            double[] y = Mapminmax_apply(x1);
            double[] a2 = Tansig_apply(y);
            x = Mapminmax_reverse(a2);

        }

        public List<decimal> DistanceReturn()
        {
            List<decimal> distance = new List<decimal>();

            for (int i = 0; i < 4; i++)
            {
                distance.Add(decimal.Round((decimal)x[i], 3));
            }

            Console.WriteLine("Distancias: \n" + distance[0] + "\n" + distance[1] + "\n" + distance[2] + "\n" + distance[3]);
            return distance;
        }

        public double[] Mapminmax_apply(double[] x1)
        {
            double[] y = new double[4];

            for (int i = 0; i < 4; i++)
            {
                y[i] = x1[i] - x1_step1_xoffset[i];
            }

            for (int i = 0; i < 4; i++)
            {
                y[i] = y[i] * x1_step1_gain[i];
            }

            for (int i = 0; i < 4; i++)
            {
                y[i] = y[i] + x1_step1_ymin;
            }


            return y; //Array Vertical

        }

        public double[] Tansig_apply(double[] y)
        {
            double[] arg = new double[10];  //numero de capas 
            double[] arg2 = new double[4];
            double[] n = new double[10]; //numero de capas 
            double[] a = new double[10]; //numero de capas 
            double[] a2 = new double[4];


            //multiplicacion del argumento (antes de la función)
            for (int i = 0; i < 10; i++)
            {
                arg[i] = (IW1_1[i, 0] * y[0]) + (IW1_1[i, 1] * y[1]) + (IW1_1[i, 2] * y[2]) + (IW1_1[i, 3] * y[3]); //Array vertical
            }

            //Suma de la matriz y del argumento (antes de la función)
            for (int i = 0; i < 10; i++)
            {
                n[i] = arg[i] + b1[i]; //Array vertical
            }

            //
            for (int i = 0; i < 10; i++)
            {
                n[i] = -2 * n[i];
                a[i] = 2 / (1 + Math.Exp(n[i])) - 1;
            }

            //a2
            double sumatoria = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    sumatoria += LW2_1[i, j] * a[j];
                }

                arg2[i] = sumatoria;
                sumatoria = 0;
            }

            for (int i = 0; i < 4; i++)
            {
                a2[i] = b2[i] + arg2[i];

            }

            return a2;

        }


        public double[] Mapminmax_reverse(double[] a2)
        {
            double[] x = new double[4];

            for (int i = 0; i < 4; i++)
            {
                x[i] = a2[i] - y1_step1_ymin;
            }

            for (int i = 0; i < 4; i++)
            {
                x[i] = x[i] / y1_step1_gain[i];
            }

            for (int i = 0; i < 4; i++)
            {
                x[i] = x[i] + y1_step1_xoffset[i];
            }

            return x;
        }

    }


}