
// control points (add as many as you like)
PVector [] P = { 
  //new PVector( 10, 380 ),
  //new PVector( 130, 60 ),
  //new PVector( 230, 300 ),
  //new PVector( 390, 10 )
  new PVector( 50, 380 ),
  new PVector( 170, 60 ),
  new PVector( 220, 280 ),
  new PVector( 320, 80 )
  //new PVector( 380, 320 )
};
PVector [] empty = {};

float u = 0.0;
float colstep;

PGraphics curveplot;

void setup()
{
  size(450,450);
  colorMode(HSB);
  colstep = 255.0 / (P.length + 1);
  // curveplot is used to remember the path of the Bezier to the current u value:
  curveplot = createGraphics(450,450);
  curveplot.beginDraw();
  curveplot.colorMode(HSB);
  curveplot.background(240);
  curveplot.smooth();
  curveplot.endDraw();
  //noLoop();  
    
}

void draw()
{
  smooth();
  // curveplot simply contains the bezier curve as calculated to the current u value:
  image(curveplot,0,0);
  // step through u:
  u += 0.002;
  //println("draw - " + u);
  if (u >= 1.0) {
    noLoop();
    curveplot.beginDraw();
    curveplot.background(240);
    curveplot.endDraw();
    u = 0.0;
  }
  // set up initial Q vector containing the control points only
  PVector [] Q = new PVector [P.length];
  for (int i = 0; i < P.length; i++) {
    Q[i] = P[i];
  }
  //ellipse(Q[0].x,Q[0].y,24,24);
  //noLoop();
  //ellipse(Q[3].x,Q[3].y,24,24);
  //delay(50);
  // now recurse though control points (col is color) 
  // with one fewer control points for each step
  float col = 0;
  while (Q.length > 0) {
    // prettification
    strokeWeight(12);
    stroke(col,255,255);
    // this simply draws the current Q vector    
    for (int i = 0; i < Q.length; i++) {
      point(Q[i].x,Q[i].y);
    }
    // prettification
    strokeWeight(2);
    // this draws the tangent vectors
    for (int j = 0; j < Q.length - 1; j++) {
      line(Q[j].x,Q[j].y,Q[j+1].x,Q[j+1].y);
    }
    // this adds a point to the memorised bezier up to the current u value:
    if (Q.length == 1) {
      curveplot.beginDraw();
      curveplot.strokeWeight(4);
      curveplot.stroke(col,255,255);
      curveplot.point(Q[0].x,Q[0].y);
      curveplot.endDraw();
      
    }
    // this recurses through de Casteljau's algorithm to calculate the points at degree d + 1:
    Q = Bezier(u,Q);
    // color for next set of points:
    col += colstep;
  }
}

// de Casteljau's algorithm
PVector [] Bezier(float u, PVector [] P)
{
  int d = P.length - 1;
  if (d == 0) {
    return empty;
  }
  // otherwise recursive calculation:
  PVector [] Q = new PVector [d];
  for (int n = 0; n < d; n++) {
    Q[n] = new PVector( 
      (1-u) * P[n].x + u * P[n+1].x,
      (1-u) * P[n].y + u * P[n+1].y
    );
  }
  return Q;
}

//void mouseDragged()
//void mouseClicked()
void mousePressed()  // при натисканні миши draw() запускається ще раз
{
   if ( mouseButton == LEFT ){
      if (looping) noLoop();
      else         loop();
      //u = 0.0;
      //loop();
      //redraw();
   }
}

/*void keyPressed()
{
   //final int k = keyCode;
   if ( key == 'p')
      if (looping) noLoop();
      else         loop();
   //if (key == 's')
   //   run = !run;
   
}*/
