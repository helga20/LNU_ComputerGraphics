int pressX_1,pressX_2,pressX_3,pressX_4,pressY_1,pressY_2,pressY_3,pressY_4;
int cnt=0;
int sym_x,sym_y;
void setup() {
  size(800, 600);
  background(230,230,230);
}

void draw() {
  if (mousePressed) {
    draw_point(mouseX,mouseY);
    delay(100);
  }
  if(keyPressed){
   if(key=='c'){
      background(230,230,230);
      cnt=0;
   }
  }
}

void draw_point(int x,int y){
  switch(cnt){
   case 0: pressX_1=x;pressY_1=y;point(x,y);cnt++;break;
   case 1: pressX_2=x;pressY_2=y;point(x,y);line(pressX_1,pressY_1,pressX_2,pressY_2);cnt++;break;
   case 2: pressX_3=x;pressY_3=y;point(x,y);line(pressX_2,pressY_2,pressX_3,pressY_3);cnt++;break;
   case 3: pressX_4=x;pressY_4=y;point(x,y);line(pressX_3,pressY_3,pressX_4,pressY_4);cnt++;draw_bez();pressX_1=pressX_4;pressY_1=pressY_4;cnt=1;func(pressX_3,pressY_3,pressX_4,pressY_4,50);draw_point(sym_x,sym_y);break;
  }
}

void draw_bez(){
  float x0=pressX_1;
  float y0=pressY_1;
  stroke(0,0,255);
  strokeWeight(4);
  smooth();
 for(int i = 0; i < 1000; i++){
   float bezX=bez(i/1000.,'x');
   float bezY=bez(i/1000.,'y');
   line(x0,y0,bezX,bezY);
   //point(bezX,bezY);
   x0=bezX;
   y0=bezY;
 }
 line(x0,y0,pressX_4,pressY_4);
 stroke(0,0,0);
 strokeWeight(1);
}

float bez(float u,char xy){
  float res=-1.0;
  if(xy=='x'){
    res=pow(1-u,3)*pressX_1+3*u*pow(1-u,2)*pressX_2+3*u*u*(1-u)*pressX_3+u*u*u*pressX_4;
  }
  else{
    res=pow(1-u,3)*pressY_1+3*u*pow(1-u,2)*pressY_2+3*u*u*(1-u)*pressY_3+u*u*u*pressY_4;
  }
  return res;
}

void func(float x, float y,float x_end,float y_end,float length){
    float v_x = x_end-x;
    float v_y = y_end-y;
    float l = sqrt(v_x * v_x + v_y * v_y);
    v_x/=l; 
    v_y/=l;
    sym_x=round(x_end+v_x*length);
    sym_y=round(y_end+v_y*length);
}
