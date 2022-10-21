float p = 0;
float p1 = 0;
float ang = PI/100; 
float w = 0;
float a = 95;
float b = 265;
float c = 435;
float d = 605;

void setup() 
{
  size(695, 695); 
  background(255); 
  stroke(167); 
  fill(255, 200, 200); 
  rectMode(CENTER);
  frameRate(20); 
}
void move()
{
  p += ang; 
  p1 -=  ang; 
  if(p>PI/2)
  {
    p=0;
  }
}
void turn_drawing_right()
{
  rotate(p); 
  rect(0, 0, w, w);
  resetMatrix();
}
void turn_drawing_left()
{
  rotate(p1); 
  rect(0, 0, w, w);
  resetMatrix(); 
}
void draw() 
{
  background(35); 
  
  /*1*/   //<>//
  translate(a, a); 
  turn_drawing_right();

  translate(b, a);
  turn_drawing_left();

  translate(a, b);
  turn_drawing_left();

  translate(b, b);
  turn_drawing_right();

  /*2*/
  translate(c, a);
  turn_drawing_right();
  
  translate(c, b); 
  turn_drawing_left();

  translate(d, a); 
  turn_drawing_left();
  
  translate(d, b); 
  turn_drawing_right();
  
  /*3*/
  translate(a, c); 
  turn_drawing_right();

  translate(b, c); 
  turn_drawing_left();

  translate(a, d); 
  turn_drawing_left(); 

  translate(b, d); 
  turn_drawing_right();
  
  /*4*/
  translate(c, c); 
  turn_drawing_right();

  translate(d, c); 
  turn_drawing_left();

  translate(c, d); 
  turn_drawing_left();

  translate(d, d); 
  turn_drawing_right();
  
  move();
  
  w=120/(sin(radians(135)-p)*sqrt(2)); 
}
