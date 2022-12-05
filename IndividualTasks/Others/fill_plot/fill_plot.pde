import java.util.Collections;
import java.util.*;
Cell[][] grid;

int cols = 33;
int rows = 33;
ArrayList<PVector> points = new ArrayList<PVector>();


void setup() 
{
  background(0,0,225);
  size(641,641);
  grid = new Cell[cols][rows];
  drawGrid();
 
  segment(4,4,4,26); 
  //segment(4,26,20,26);
  segment(20,26,28,18);
  segment(28,18,21,4);
  segment(21,4,21,8);
  //segment(21,8,10,8);
  segment(10,8,10,4);
  //segment(10,4,4,4);
  
  //dispalyPolygon();
  
  segment(10,12,10,20);
  //segment(10,20,17,20);
  segment(17,20,21,16);
  segment(21,16,21,12);
  //segment(21,12,10,12);
  
  fillingAlg(); 
  //dispalyPolygon();
  
}
void fillingAlg()
{
  float start = minY();
  float finish = maxY();
 
  ArrayList<PVector> resPoints = new ArrayList<PVector>();
  ArrayList<PVector> resMainPoints = new ArrayList<PVector>();
  
  for(float i = finish; i >= start; i-= 20)
  {
     ArrayList<PVector> partOfRes = getItersectionPoints(i);
     Collections.sort(partOfRes, new CompareToX());
     resMainPoints.addAll(partOfRes);    
  }
 
  for(int i = 0; i < resMainPoints.size() - 1; i= i + 2) //<>//
  {
    float x = resMainPoints.get(i).x;
    float y = resMainPoints.get(i).y;
     
     while( x <= resMainPoints.get(i + 1).x && y <= resMainPoints.get(i + 1).y )
     {
        resPoints.add(new PVector(x, y));
        x+= 20;
     }
  }
  
  fill(123,0,123);
  for (PVector point : resPoints) 
  {
      rect(point.x - 20, 640 - point.y, 20, 20);
      print(point.x + "\t" + point.y + "\n");
  }
  delay(100);
}
ArrayList<PVector> getItersectionPoints(float y)
{
  ArrayList<PVector> res = new ArrayList<PVector>();
  for (PVector point : points) 
  {
     if(point.y == y)
     {
        res.add(new PVector(point.x, point.y));
     }    
  }
  
  return res;
}
float minY()
{
  float min = points.get(0).y;
   for (PVector point : points) 
  {
      if(min > point.y)
      {
         min = point.y;
      }
  }
  return min;
}
float maxY()
{
  float max = points.get(0).y;
   for (PVector point : points) 
  {
      if(max < point.y)
      {
        max = point.y;
      }
  }
  return max;
}
void segment(int x0, int y0, int x1, int y1)
{
  int dx = abs(x1 - x0);
  int dy = abs(y1 - y0);
  int sx = x1 >= x0 ? 1 : -1;
  int sy = y1 >= y0 ? 1 : -1;
  
  if (dy <= dx)
  {
    int d = (dy << 1) - dx;
    int d1 = dy << 1;
    int d2 = (dy - dx) << 1;
    
    if(!points.contains(new PVector(x0*20, y0*20)))
    { 
       points.add(new PVector(x0*20, y0*20));
    }
    
    for(int x = x0 + sx, y = y0, i = 1; i <= dx; i++, x += sx)
    {
      if ( d >0)
      {
        d += d2;
        y += sy;
      }
      else
       { 
         d += d1;
       }
       
      if(!points.contains(new PVector(x*20, y*20)))
      { 
         points.add(new PVector(x*20, y*20));
      }
      
    }
  }
  else
  {
    int d = (dx << 1) - dy;
    int d1 = dx << 1;
    int d2 = (dx - dy) << 1;
    
    if(!points.contains(new PVector(x0*20, y0*20)))
    { 
       points.add(new PVector(x0*20, y0*20));
    }
    
    for(int y = y0 + sy, x = x0, i = 1; i <= dy; i++, y += sy)
    {
      if ( d >0)
      {
        d += d2;
        x += sx;
      }
      else
      { 
         d += d1;
     }
     
     if(!points.contains(new PVector(x*20, y*20)))
     { 
         points.add(new PVector(x*20, y*20));
     }
     
    }
  }
}
class Cell 
{  
  float x,y;   
  float w,h;   
 
  Cell(float tempX, float tempY, float tempW, float tempH) {
    x = tempX;
    y = tempY;
    w = tempW;
    h = tempH;   
  } 
 
  void display() {
    stroke(0,120,180);
    fill(230);
    rect(x,y,w,h); 
  }
}
void drawGrid()
{
   for (int i = 0; i < cols; i++) 
   {
      for (int j = 0; j < rows; j++) 
      {
        grid[i][j] = new Cell(i*20,j*20,20,20);
      }
   }
  
   for (int i = 0; i < cols; i++) 
   {
      for (int j = 0; j < rows; j++) 
      {
        grid[i][j].display();
      }
   }
}
void dispalyPolygon()
{
  fill(123);
  for (PVector point : points) 
  {
      rect(point.x - 20 , 640 - point.y, 20, 20);
  }
}
class CompareToX implements Comparator<PVector>
{
  //@Override
  int compare(PVector v1, PVector v2)
  {
    return int(v1.x - v2.x);
  }
}