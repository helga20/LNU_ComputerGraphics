class Point 
{

  float x;
  float y;
  float diametr;
  

  Point(float x_, float y_) 
  {
    x = x_;
    y = y_;
    diametr = 15;
    
  }

  void display() 
  {
    stroke(0);
    strokeWeight(1);
    fill(127,100);
    circle(x, y, diametr);
  }
}

Point[] points = new Point[100];
int total = 0;


void setup() 
{
  size(1000, 700);
}

void mousePressed() 
{
   points[total] = new Point(mouseX, mouseY);
   total = total + 1;
}

void keyPressed() {
  total = total - 1;
}




int Factorial(int number)
{
  int result = 1;
  for (int i = 1; i <= number; i++)
  {
     result *= i;
  }
  return result;
}
float Polinom(int i, int degree, float t)
{
    return (Factorial(degree) / (Factorial(i) * Factorial(degree - i))) * (float)pow(t, i) * (float)pow(1 - t, degree - i);
}

float X_calculation(float t)
{
    float x = 0;
    for (int i = 0; i <= total -1; i++)
    {
       x += Polinom(i, total -1, t) * points[i].x;
    }
    return x;
}

float Y_calculation(float t)
{
    float y = 0;
    for (int i = 0; i <= total - 1; i++)
    {
      y += Polinom(i, total - 1, t) * points[i].y;
    }
    return y;
}
void BezierCurve()
{
  float t = 0;
  float x_new;
  float y_new;
  while(t<=1)
  {
    x_new = X_calculation(t);
    y_new = Y_calculation(t);
    //stroke(155, 0, 200);
    stroke(0);
    strokeWeight(5);
    point(x_new, y_new);
    t += 0.00001;
  }
}

void draw() {
  background(255);
  String position = "X: " + mouseX + "   Y: " + mouseY;
  textSize(20);
  fill(0);
  text(position, 20, 670);
  
  for (int i = 0; i < total; i++) 
  {
    points[i].display();
    
  }
  for(int i = 1; i<total; i++)
  {
    //stroke(0, 0, 139);
    stroke(255, 165, 0);
    strokeWeight(3);
    line(points[i].x, points[i].y, points[i-1].x, points[i-1].y);
  }
  if(total >= 2)
  {
    BezierCurve();
  }
  
}
