float [][] B1 = { //P(u, 0)
  {0, 0, 0}, 
  {1, 1, 0}, 
  {2, 1, 0}, 
  {3, 0, 0}, 
  {3, 0, 3} };

float [][] B2 = { //P(u, 1)
  {0, 0, 3}, 
  {1, 1, 3}, 
  {2, 1, 3}, 
  {3, 0, 0}, 
};

float [][] C1 = { //P(0, w)
  {0, 0, 3}, 
  {0, 1, 2}, 
  {0, 1, 1}, 
  {0, 0, 0}, 
};

float [][] C2 = { //P(1, w)
  {3, 0, 3}, 
  {3, 1, 2}, 
  {3, 1, 2}, 
  {3, 1, 1}, 
  {3, 0, 0}
};

int click = 0; // mouseclick
float u, w; // points on surface
float t = 0;
float s = 0;

float[] X={0, 0, 0, 0, 1, 2, 3, 3, 3}; //vector of node for C2, B1
float[] Y={0, 0, 0, 0, 1, 2, 2, 2}; //vector of node for C1, B2

float[][] Pu0=new float[100][3]; //P(u, 0)
float[][] Pu1=new float[100][3]; //P(u, 1)
float[][] P0w=new float[100][3]; //P(0, w)
float[][] P1w=new float[100][3]; //P(1, w)
float[][][] Q=new float[100][100][3]; //result array with points

float[] P00 = new float[]{0, 0, 0 }; // limit point P(0, 0)
float[] P01 = new float[]{0, 0, 3 }; // limit point P(0, 1)
float[] P10 = new float[]{3, 3, 0 }; // limit point P(1, 0)
float[] P11 = new float[]{3, 3, 3 }; // limit point P(1, 1)

float [][] Nt=new float[6][4]; // normalized functions b splines(C2, B1)
float [][] Ns=new float[5][4]; // normalized functions b splines(C1, B2)

void setup()
{
  size(1200, 750, P2D);
}

void draw()
{
  background(255);
  for (int num1 = 0; num1 < 100; num1++)
  {
    w = num1 * 0.01;
    t = w * 3; //normalizerd parametr
    s = w * 2; //normalizerd parametr
    Nt = makeNFunction(t, X, 6, 4); //for P(0,w)
    Ns = makeNFunction(s, Y, 5, 4); //for P(1,w)

    for (int j = 0; j < 3; j++)
    {
      for (int i = 0; i < 100; i++)
      {
        P0w[i][j] = 0;
        P1w[i][j] = 0;
      }
    }

    for (int j = 0; j < 3; j++) //P(0,w)
      for (int i = 0; i < 4; i++)
        P0w[num1][j] += C1[i][j] * Ns[i][3];

    for (int j = 0; j < 3; j++) //P(1,w)
      for (int i = 0; i < 5; i++)
        P1w[num1][j] += C2[i][j] * Nt[i][3];

    for (int num2 = 0; num2 < 100; num2++)
    {
      u = num2 * 0.01;
      t = u * 3;
      s = u * 2;

      Nt = makeNFunction(t, X, 6, 4); //for P(u, 0)
      Ns = makeNFunction(s, Y, 5, 4); //for P(u, 1)

      for (int j = 0; j < 3; j++)
      {
        for (int i = 0; i < 100; i++)
        {
          Pu0[i][j] = 0;
          Pu1[i][j] = 0;
        }
      }

      for (int j = 0; j < 3; j++) //for P(u, 0)
        for (int i = 0; i < 5; i++)
          Pu0[num2][j] += B1[i][j] * Nt[i][3];

      for (int j = 0; j < 3; j++) //for P(u, 1)
        for (int i = 0; i < 4; i++)
          Pu1[num2][j] += B2[i][j] * Ns[i][3];

      for (int j = 0; j < 3; j++) //result matrix
      {
        Q[num2][num1][j] = Pu0[num2][j] * (1 - w) + Pu1[num2][j] * w + P0w[num1][j] * (1 - u) +
          P1w[num1][j] * u - P00[j] * (1 - u) * (1 - w) + P01[j] * (1 - u) * w - P10[j] * u * (1 - w) - P11[j] * u * w;
      }
    }
  }

  if (click == 0)
  {
    markup("x = 0");
    int i  = 0;
    int j = 0;
    for (i = 0; i < 100; i++)
      for (j = 0; j < 100; j++)
      {
        point((int)(Q[i][j][2] * 100), (int)(Q[i][j][1] * 100));
      }
              print(Q[i-1][j-1][0], Q[i-1][j-1][1], Q[i-1][j-1][2]);
  }
  if (click == 1)
  {
    markup("y = 0");
    for (int i = 0; i < 100; i++)
      for (int j = 0; j < 100; j++)
        point((int)(Q[i][j][2] * 100), (int)(Q[i][j][0] * 100));
  }
  if (click == 2)
  {
    markup("z = 0");
    for (int i = 0; i < 100; i++)
      for (int j = 0; j < 100; j++)
        point((int)(Q[i][j][1] * 100), (int)(Q[i][j][0] * 100));
  }
}

void markup(String str)
{
  stroke(256, 0, 0);
  textSize(18);
  fill(204, 102, 0);
  text(str, 10, 100);
  translate(400, 300);
  stroke(204, 102, 0);
}
void mousePressed()
{
  click++;
  if (click > 2)
    click = 0;
}

float [][] makeNFunction(float t, float[] X, int rows, int cols)
{
  float[][] N = new float[rows][cols];
  for (int k = 0; k < cols; k++)
  {
    for (int i = 0; i < rows; i++)
    {
      N[i][k] = 0;
    }
  }

  for (int i = 0; i < rows; i++)
  {
    if (t >= X[i] && t < X[i + 1])
    {
      N[i][1] = 1;
    }
  }

  for (int k = 2; k < cols; k++)
  {
    for (int i = 0; i < rows; i++)
    {
      if (X[i + k - 1] - X[i] != 0)
        N[i][k] += (t - X[i]) * N[i][k - 1] / (X[i + k - 1] - X[i]);
      if (X[i + k] - X[i + 1] != 0)
        N[i][k] += (X[i + k] - t) * N[i + 1][k - 1] / (X[i + k] - X[i + 1]);
    }
  }
  return N;
}