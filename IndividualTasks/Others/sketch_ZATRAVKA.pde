IntList lstX;
IntList lstY;

IntList lX;
IntList lY;

Draw D;
Stack s;

Pair seed;
Pair current;

color c;
color new_value=#CCEACC;
color limit_value=#7D4483;

int seed_X=14;
int seed_Y=21;

int h;
int k=0;
int d=0;

int currStep = 0;

void setup()
{
  size(1400, 660);
}

void draw()
{
  background(255);
  
  lstX=new IntList();
  lstY=new IntList();
  
  lX=new IntList();
  lY=new IntList();
  
  D=new Draw();
  D.DrawBackground();
  
  //Seed Algorithm
  
  s=new Stack();
  seed=new Pair(seed_X, seed_Y);   //SETTING of seed pixel
  current=new Pair();
  
  current=seed;
  
  s.Push(current);
  lstX.append(current.x);
  lstY.append(current.y);
  
  
  int step = 0;
  while(!s.IsEmpty() && step < currStep)
  {
    step++;
    Pair currentSeed = s.Pop();
    
    D.DrawPixel(currentSeed.x, currentSeed.y, new_value);
    
    current = new Pair(currentSeed.x+1, currentSeed.y);
    
    while(get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=limit_value &&
          get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=new_value)
    {
      if (get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=color(100,255,100))//temp seet
      {
        D.DrawPixel(current.x, current.y, new_value);
      }
      current.x++;
    }
    
    int right = current.x-1;
    current = new Pair(currentSeed.x-1, currentSeed.y);
    
    while(get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=limit_value &&
          get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=new_value)
    {
      if (get(Convert.ToPixelX(current.x), Convert.ToPixelY(current.y))!=color(100,255,100))//temp seet
      {
        D.DrawPixel(current.x, current.y, new_value);
      }
      current.x--;
    }
    
    int left = current.x+1;
    
    int rightTop = right;
    int rightBottom = right;
    
    while(rightTop > left && (
          get(Convert.ToPixelX(rightTop), Convert.ToPixelY(current.y+1))==limit_value ||
          get(Convert.ToPixelX(rightTop), Convert.ToPixelY(current.y+1))==new_value))
    {
      rightTop--;
    }
    while(rightBottom > left && (
          get(Convert.ToPixelX(rightBottom), Convert.ToPixelY(current.y-1))==limit_value ||
          get(Convert.ToPixelX(rightBottom), Convert.ToPixelY(current.y-1))==new_value))
    {
      rightBottom--;
    }
    
    int leftTop = left;
    int leftBottom = left;
    
    while(leftTop < right && (
          get(Convert.ToPixelX(leftTop), Convert.ToPixelY(current.y+1))==limit_value ||
          get(Convert.ToPixelX(leftTop), Convert.ToPixelY(current.y+1))==new_value))
    {
      leftTop++;
    }
    while(leftBottom < right && (
          get(Convert.ToPixelX(leftBottom), Convert.ToPixelY(current.y-1))==limit_value ||
          get(Convert.ToPixelX(leftBottom), Convert.ToPixelY(current.y-1))==new_value))
    {
      leftBottom++;
    }
    
    if (leftTop <= rightTop &&
        get(Convert.ToPixelX(rightTop), Convert.ToPixelY(current.y+1))!=limit_value &&
        get(Convert.ToPixelX(rightTop), Convert.ToPixelY(current.y+1))!=new_value)
    {
      s.Push(new Pair(rightTop, current.y+1));
      lstX.append(rightTop);
      lstY.append(current.y+1);
      D.DrawPixel(rightTop, current.y+1, color(100,255,100));
    }
    if (leftTop < rightTop &&
        get(Convert.ToPixelX(leftTop), Convert.ToPixelY(current.y+1))!=limit_value &&
        get(Convert.ToPixelX(leftTop), Convert.ToPixelY(current.y+1))!=new_value)
    {
      s.Push(new Pair(leftTop, current.y+1));
      lstX.append(leftTop);
      lstY.append(current.y+1);
      D.DrawPixel(leftTop, current.y+1, color(100,255,100));
    }
    if (leftBottom <= rightBottom &&
        get(Convert.ToPixelX(rightBottom), Convert.ToPixelY(current.y-1))!=limit_value &&
        get(Convert.ToPixelX(rightBottom), Convert.ToPixelY(current.y-1))!=new_value)
    {
      s.Push(new Pair(rightBottom, current.y-1));
      lstX.append(rightBottom);
      lstY.append(current.y-1);
      D.DrawPixel(rightBottom, current.y-1, color(100,255,100));
    }
    if (leftBottom < rightBottom &&
        get(Convert.ToPixelX(leftBottom), Convert.ToPixelY(current.y-1))!=limit_value &&
        get(Convert.ToPixelX(leftBottom), Convert.ToPixelY(current.y-1))!=new_value)
    {
      s.Push(new Pair(leftBottom, current.y-1));
      lstX.append(leftBottom);
      lstY.append(current.y-1);
      D.DrawPixel(leftBottom, current.y-1, color(100,255,100));
    }
    
  }
  //END of algorithms
  
  D.DrawPixel(seed_X, seed_Y, 0);
  fill(255);
  text("S", 180+seed_X*20+6, 20+seed_Y*20+15);
  noFill();
    
  if(keyPressed==true)
  {
    D.DrawContentOfStack();
  }
  else
  {
    D.DrawLimitPixels();
  }
  
}

void keyPressed ()
{
 if (key == ' ')
 {
   currStep++;
 }
}

static class Convert
{
  static int ToPixelX(int x) //get in pixels from 0 to 31
  {
    return 180+x*20+10;
  }
  static int ToPixelY(int y)
  {
    return 20+y*20+10;
  }
}


class Pair
{
  int x;
  int y;
  Pair()
  {
    x=y=0;
  }
  Pair(int xx, int yy)
  {
    x=xx;
    y=yy;
  }
}

class Node
{
  Pair pair;
  Node prev;
  Node()
  {
    pair=null;
    prev=null;
  }
  Node(Pair p)
  {
    pair=p;
  }
}

class Stack
{
  Node head;
  
  Stack()
  {
    head=null;
  }
  
  public Pair Pop()
  {
    Pair temp=new Pair(head.pair.x, head.pair.y);
    
    head=head.prev;
    
    return temp;
  }
  
  public void Push(Pair p)
  {
    Node temp=new Node(p);
    temp.prev=head;
    head=temp;
  }
  
  public Pair Top()
  {
    return head.pair;
  }
  
  public boolean IsEmpty()
  {
    if(head==null)
    return true;
    else
    return false;
  }
}


//class Draw
class Draw
{
  public void DrawBackground()
  {
    rect(180, 20, 640, 640);
    for(int i=1;i<=32; i++)
    {
      line(180+20*i, 20, 180+20*i, 660);
      line(180, 20+20*i, 820, 20+20*i);
    }
    
    for(int i=0;i<32;i++)
    {
      fill(0);
      textSize(15);
      text(i, 180+20*i, 18);
      text(i, 160, 35+20*i);
      noFill();
    }
    
    
    //FILLING of polygons
    for(int i=4; i<=26; i++)
    {
      lX.append(4);
      lY.append(i);
      DrawPixel(4, i, limit_value);
    }
    
    for(int i=4;i<=20;i++)
    {
      lX.append(i);
      lY.append(26);
      DrawPixel(i, 26, limit_value);
    }
    
    for(int i=1; i<=8;i++)
    {
      lX.append(20+i);
      lY.append(26-i);
      DrawPixel(20+i, 26-i, limit_value);
    }
    
    for(int i=0;i<=6;i++)
    {
      for(int j=0;j<=1;j++)
      {
        lX.append(27-i);
        lY.append(17-2*i-j);
        DrawPixel(27-i, 17-2*i-j, limit_value);
      }
    }
    
    /*for(int i=1;i<=17; i++) //for check
    {                       //
      lX.append(21-i);      //
      lY.append(4);          //
                             //
      DrawPixel(21-i, 4, limit_value); //
    }*/                                 //
    
    for(int i=1;i<=4; i++)
    {
      lX.append(21);
      lY.append(4+i);
      DrawPixel(21, 4+i, limit_value);
    }
    
    for(int i=1;i<=11; i++)
    {
      lX.append(21-i);
      lY.append(8);
      DrawPixel(21-i, 8, limit_value);
    }
    
    for(int i=1;i<=4; i++)
    {
      lX.append(10);
      lY.append(8-i);
      DrawPixel(10, 8-i, limit_value);
    }
    
    for(int i=1;i<=6; i++)
    {
      lX.append(10-i);
      lY.append(4);
      DrawPixel(10-i, 4, limit_value);
    }
    
    //inner polygon
    
    for(int i=0;i<=8; i++)
    {
      lX.append(10);
      lY.append(12+i);
      DrawPixel(10, 12+i, limit_value);
    }
    
    for(int i=1;i<=7; i++)
    {
      lX.append(10+i);
      lY.append(20);
      DrawPixel(10+i, 20, limit_value);
    }
    
    for(int i=1;i<=4; i++)
    {
      lX.append(17+i);
      lY.append(20-i);
      DrawPixel(17+i, 20-i, limit_value);
    }
    
    for(int i=1;i<=4; i++)
    {
      lX.append(21);
      lY.append(16-i);
      DrawPixel(21, 16-i, limit_value);
    }
    
    for(int i=1;i<=11; i++)
    {
      lX.append(21-i);
      lY.append(12);
      DrawPixel(21-i, 12, limit_value);
    }
  }
  
  public void DrawPixel(int x, int y, int c)
  {
    fill(c);
    rect(180+20*x, 20+20*y, 20, 20);
    noFill();
  }
  
  public void DrawContentOfStack()
  {
    rect(825, 20, 550, 960);
    fill(0);
    text("Contnents of the Stack", 830, 35);
    noFill();
    
    
  int h=0;
  int j=0;
  for(int i=0;i<lstX.size(); i++)
  {
    if(i%50==0)
    {
      h++;
      j=0;
    }
    fill(0);
    textSize(9);
    text(lstX.get(i)+"  "+lstY.get(i), 800+45*h, 50+12*j);
    j++;
    noFill();
  }
  }
  
  public void DrawLimitPixels()
  {
     rect(825, 20, 200, 960);
    fill(0);
    text("Limit pixels", 830, 35);
    noFill();
    
    
  int h=0;
  int j=0;
  for(int i=0;i<lX.size(); i++)
  {
    if(i%50==0)
    {
      h++;
      j=0;
    }
    fill(0);
    textSize(9);
    text(lX.get(i)+"  "+lY.get(i), 800+45*h, 50+12*j);
    j++;
    noFill();
  }
  }
}
