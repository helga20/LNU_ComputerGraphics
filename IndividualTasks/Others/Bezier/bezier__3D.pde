PVector [] P = new PVector [4]; //РІРµРєС‚РѕСЂ РґРµ 4 С‚РѕС‡РєРё (x,y)
PVector [] empty = {};
IntList X = new IntList();;
IntList Y = new IntList();
int count = 0;
float u = 0.0;


void setup()
{
  size(800,800,P3D); 
  background(240); 
  smooth();    
}

void draw()
{
  if ( mousePressed){
    draw_point(mouseX,mouseY);
    delay(200);
    if (count == 4){
      if ( mouseButton == RIGHT ){
      draw_line();
      }
    }
  }
  if (keyPressed){
     if(key=='b'){
       draw_bezier();
     }
  }
}

void draw_point (int x, int y) {
   switch(count){
   case 0: X.append(x);Y.append(y);ellipse(X.get(0),Y.get(0),8,8);count++;break;
   case 1: X.append(x);Y.append(y);ellipse(X.get(1),Y.get(1),8,8);count++;break;
   case 2: X.append(x);Y.append(y);ellipse(X.get(2),Y.get(2),8,8);count++;break;
   case 3: X.append(x);Y.append(y);ellipse(X.get(3),Y.get(3),8,8);count++;break;
  }
}

void draw_line(){
  line(X.get(0),Y.get(0),X.get(1),Y.get(1));
  line(X.get(1),Y.get(1),X.get(2),Y.get(2));
  line(X.get(2),Y.get(2),X.get(3),Y.get(3));

}

void draw_bezier() {

  for (int i = 0; i < 4; i++) 
     P[i] = new PVector(X.get(i),Y.get(i)); 
     
  while (u <= 1)
  {
    u += 0.002;
    PVector [] Q = new PVector [P.length];
    for (int i = 0; i < P.length; i++) 
    {
      Q[i] = P[i];
    }
    while (Q.length > 0) 
    {    
      if (Q.length == 1) 
      {
        strokeWeight(4);
        stroke(151,10,0);
        point(Q[0].x,Q[0].y);        
      }
    
      Q = Bezier(u,Q);

     }
  }
}

//РђР»РіРѕСЂРёС‚Рј РґРµ РљР°СЃС‚РµР»СЊР¶Рѕ
PVector [] Bezier(float u, PVector [] P)
{
  int d = P.length - 1;
  if (d == 0) {
    return empty;
  }

  PVector [] Q = new PVector [d];
  for (int n = 0; n < d; n++) {
    Q[n] = new PVector( 
      (1-u) * P[n].x + u * P[n+1].x,
      (1-u) * P[n].y + u * P[n+1].y
    );
  }
  return Q;
}
