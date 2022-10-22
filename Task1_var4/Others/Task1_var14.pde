float r = 0;
float x = 50, y = 50;

void rot_rect(float X, float Y, float R){
  x = X;
  y = Y;
  r = R;
  x = x +1;
  float leng = 200;
  translate(x, y);
  
  if(x + 53 == leng){
    rotate(r);
    r= 0;
    stop();
  }else{
    r = r - 0.05; 
  }
    pushMatrix();
    rotate(r);
    rect(0, 0, 100, 100);
    popMatrix();
    resetMatrix();
    line(0, y, leng, y);
    line(leng, 0, leng, 400);
}


void setup() {
  size(400, 400);
  background(192, 192, 192);
  rectMode(CENTER);
  stroke(0, 0, 0);
}


void draw() {
  rot_rect(x, y, r);
}
