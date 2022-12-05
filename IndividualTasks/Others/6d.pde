void setup() 
{ 
  size(1200,650);
  background(255);
}

void draw()
{
 floatingHorizonAlgorithm();
}

float R(float x, float z)
{
  return sqrt(x * x + z * z);
}

float yFirst(float x, float z)
{
  float currR = R(x, z);
  return (8 * cos(1.2 * currR) / (float)(currR + 1));
}

float ySecond(float x, float z)
{
  float currR = R(x, z);
  return (16 * (sin(1.2 * currR) + cos(1.5 * currR)) / (float)(currR + 1));
}

float[] rotateAroundX(float[] point, float xAngle)
{
  float xRad = xAngle * (float)PI / 180;
  
  float[][] xRotation = new float[][]
  {
    { 1,  0,                0               , 0},
    { 0, (float)cos(xRad), -(float)sin(xRad), 0},
    { 0, (float)sin(xRad),  (float)cos(xRad), 0},
    { 0, 0,                 0,                1}
  };
  
  float[] result = new float[point.length];
  
  for (int i = 0; i < point.length; i++)
  {
    float res = 0;
    for (int j = 0; j < point.length; j++)
    {
      res += point[j] * xRotation[j][i];
    }
    result[i] = res;
  }  
  
  return result;
}

float[] rotateAroundY(float[] point, float yAngle)
{
  float yRad = yAngle * (float)PI / 180;
  
  float[][] yRotation = new float[][]
  {
    { (float)cos(yRad),  -(float)sin(yRad), 0, 0},
    { (float)sin(yRad),  (float)cos(yRad),  0, 0},
    { 0,                 0,                 1, 0},
    { 0,                 0,                 0, 1}
  };
  
  float[] result = new float[point.length];
  
  for (int i = 0; i < point.length; i++)
  {
    float res = 0;
    for (int j = 0; j < point.length; j++)
    {
      res += point[j] * yRotation[j][i];
    }
    result[i] = res;
  }  
  
  return result;
}

void floatingHorizonAlgorithm()
{
  float scaleX = 45;
  float scaleY = 25;
  
  float stepsX = 0.001;
  float stepsZ = 0.3;
  
  int n = 12567;
  float[] ymax1 = new float[n];
  float[] ymin1 = new float[n];
 
  float[] ymax2 = new float[n];
  float[] ymin2 = new float[n];
  
  float[] surface1 = new float[4];
  float[] surface2 = new float[4];
  
  float xAngle = 35;
  float yAngle = 5;
 
  int i = 0;
  for(float x = -2 * PI; x < 2 * PI; x += stepsX)
  {
    float z = -2 * PI;
    float y1 = yFirst(x, z);
    float y2 = ySecond(x, z);
   
    surface1[0] = x;
    surface1[1] = y1;
    surface1[2] = z;
    surface1[3] = 1;
   
    surface1 = rotateAroundX(surface1, xAngle);
    surface1 = rotateAroundY(surface1, yAngle);

    ymax1[i] = surface1[1];
    ymin1[i] = surface1[1];
   
    surface2[0] = x;
    surface2[1] = y2;
    surface2[2] = z;
    surface2[3] = 1;
   
    surface2 = rotateAroundX(surface2, xAngle);
    surface2 = rotateAroundY(surface2, yAngle);
   
    ymax2[i] = surface2[1];
    ymin2[i] = surface2[1];
   
    i++;
  }
 
  for(float z = -2 * PI; z < 2 * PI; z += stepsZ) 
  { 
    int j = 0;
    for(float x = -2 * PI ; x < 2*PI; x += stepsX) 
    {           
      float y1 = yFirst(x, z);
      float y2 = ySecond(x, z);
            
      surface1[0] = x; 
      surface1[1] = y1; 
      surface1[2] = z;
      surface1[3] = 1;
      
      surface1 = rotateAroundX(surface1, xAngle);
      surface1 = rotateAroundY(surface1, yAngle);
  
      surface2[0] = x; 
      surface2[1] = y2; 
      surface2[2] = z;
      surface2[3] = 1;
      
      surface2 = rotateAroundX(surface2, xAngle);
      surface2 = rotateAroundY(surface2, yAngle);

      if(surface1[1] >= ymax1[j] && surface1[1] >= ymax2[j])
      {
        stroke(255, 0, 0);
        ymax1[j] = surface1[1];
        ymax2[j] = surface1[1];
        point(scaleX * surface1[0] + 600, scaleY * surface1[1] + 250);
      }
      else if(surface1[1] <= ymin1[j] && surface1[1] <= ymin2[j])
      {
         stroke(255, 0 ,0);
         ymin1[j] = surface1[1];
         ymin2[j] = surface1[1];
         point(scaleX * surface1[0] + 600, scaleY * surface1[1] + 250);
      }
       
      if(surface2[1] >= ymax2[j] && surface2[1] >= ymax1[j])
      {
        stroke(0);
        ymax2[j] = surface2[1];
        ymax1[j] = surface2[1];
        point(scaleX * surface2[0] + 600, scaleY * surface2[1] + 250);
      }
      else if(surface2[1] <= ymin2[j] && surface2[1] <= ymin1[j])
      {
         stroke(0);
         ymin2[j] = surface2[1];
         ymin1[j] = surface2[1];
         point(scaleX * surface2[0] + 600, scaleY * surface2[1] + 250);
       }
       j++;
    }
  }
}
                 