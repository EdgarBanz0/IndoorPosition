namespace IndoorPositionApp.Pages
{
    class EstimatePosition
    {
        readonly decimal[,] AtAinv = new decimal[2, 2];
        readonly decimal[] AtC = new decimal[2];

        public decimal[] Matrix(decimal[,] Coord, decimal[] Dist)
        {
            decimal[,] A = new decimal[Coord.GetLength(0) - 1, 2];
            decimal[] C = new decimal[Coord.GetLength(0) - 1];
            decimal[,] AtA = new decimal[2, 2];

            for (int i = 1; i < Coord.GetLength(0); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    A[i - 1, j] = 2 * (Coord[i, j] - Coord[0, j]);
                }
                C[i - 1] = Dist[0] - Dist[i] - (Coord[0, 0] * Coord[0, 0]) + (Coord[i, 0] * Coord[i, 0]) - (Coord[0, 1] * Coord[0, 1]) + (Coord[i, 1] * Coord[i, 1]);
            }

            decimal aux = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < Coord.GetLength(0) - 1; k++)
                    {
                        aux += A[k, i] * A[k, j];
                    }
                    AtA[i, j] = aux;
                    aux = 0;
                }
            }

            decimal detAtA = AtA[0, 0] * AtA[1, 1] - AtA[1, 0] * AtA[0, 1];

            AtAinv[0, 0] = AtA[1, 1] / detAtA;
            AtAinv[0, 1] = (-AtA[0, 1]) / detAtA;
            AtAinv[1, 0] = (-AtA[1, 0]) / detAtA;
            AtAinv[1, 1] = AtA[0, 0] / detAtA;

            for (int j = 0; j < 2; j++)
            {
                aux = 0;
                for (int k = 0; k < Coord.GetLength(0) - 1; k++)
                {
                    aux += A[k, j] * C[k];
                }
                AtC[j] = aux;

            }

            decimal X = AtAinv[0, 0] * AtC[0] + AtAinv[0, 1] * AtC[1];
            decimal Y = AtAinv[1, 0] * AtC[0] + AtAinv[1, 1] * AtC[1];

            decimal[] XYDis = { decimal.Round(X, 2), decimal.Round(Y, 2) };
            return XYDis;

        }
    }
}
