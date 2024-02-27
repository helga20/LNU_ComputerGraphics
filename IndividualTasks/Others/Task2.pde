int click = 0;

int steps = 15;

PVector[] vertexes = { new PVector(-60.0,   7.0,  40.0), new PVector( 58.0, -96.0, -40.0), new PVector(-50.0,   9.0, -50.0), new PVector( 83.0,  23.0,  30.0) };
//PVector[] vertexes = { new PVector(-50.0,   25.0,  50.0), new PVector( 50.0, -25.0, 50.0), new PVector(-50.0,   -50.0, -25.0), new PVector( 25.0,  25.0,  25.0) };

float[] margins = new float[steps + 1];

PVector[][] surface = new PVector[steps + 1][steps + 1];

PVector bilinear_equation(float u, float v)
{
  return new PVector(vertexes[0].x * (1 - u) * (1 - v) + vertexes[1].x * (1 - u) * v + vertexes[2].x * u * (1 - v) + vertexes[3].x * u * v,
                     vertexes[0].y * (1 - u) * (1 - v) + vertexes[1].y * (1 - u) * v + vertexes[2].y * u * (1 - v) + vertexes[3].y * u * v,
                     vertexes[0].z * (1 - u) * (1 - v) + vertexes[1].z * (1 - u) * v + vertexes[2].z * u * (1 - v) + vertexes[3].z * u * v);
}

void setup()
{
  size(800, 800, P3D);
}

void mousePressed()
{
  click += 1;
  
  if (click > 3)
  {
    click = 0;
  }
}

void draw()
{
  // Очищення екрану при кожному оновленні фрейму
  background(255);
  
  // Вивід теперішнього способу проекції
  textSize(32);
  fill(255, 0 ,0);
  if (click == 0)
  {
    text("Bilinear surface: ", 20 , 40);
  }
  if (click == 1)
  {
    text("X = 0 (Projection)", 20 , 40);
  }
  if (click == 2)
  {
    text("Y = 0 (Projection)", 20 , 40);
  }
  if (click == 3)
  {
    text("Z = 0 (Projection)", 20 , 40);
  }
  
  // Налаштування камери (з можливістю рухати нею)
  if (click == 0)
  {
    camera(mouseX, mouseY, (height / 2) / tan(PI / 6), width / 2, height / 2, 0, 0, 1, 0);
  }
  else
  {
    camera(height / 2, width / 2, (height / 2) / tan(PI / 6), width / 2, height / 2, 0, 0, 1, 0);
  }
  
  // Налаштування фігури (переміщення -> поворот -> збільшення)
  translate(width / 2, height / 2);
  rotateX(PI / 2);
  //rotateZ(PI / 2);
  scale(3);
  
  if (click == 0)
  {
    stroke(255, 0, 0);
    strokeWeight(2);
    beginShape(POINTS);
    vertex(-60.0,   7.0,  40.0);
    vertex(58.0, -96.0, -40.0);
    vertex(83.0,  23.0,  30.0);
    vertex(-50.0,   9.0, -50.0);
    endShape();
    strokeWeight(1);
    stroke(0, 0, 0);
  }
  
  // Координатні осі
  beginShape(LINES);
  vertex(-150, 0, 0);
  vertex( 150, 0, 0);
  vertex(0, -150, 0);
  vertex(0,  150, 0);
  vertex(0, 0, -150);
  vertex(0, 0,  150);
  endShape();
  
  // Обчислення, позиціювання точок та заповнення поверхонь
  for(int i = 0; i <= steps; i++)
  {
    margins[i] = (1.0 / steps) * i;
  }
  
  for(int u = 0; u <= steps; u++)
  {
    for(int v = 0; v <= steps; v++)
    {
      surface[u][v] = bilinear_equation(margins[u], margins[v]);
    }
  }

  for(int u = 0; u < steps; u++)
  {
    if (click == 0)
    {
      beginShape(POINTS);
      for(int v = 0; v <= steps; v++)
      {
          fill(
               ((255 / steps) * u) + surface[u][v].x, 
               ((255 / steps) * u) + surface[u][v].y, 
               ((255 / steps) * u) + surface[u][v].z
               );
          vertex(surface[u][v].x, surface[u][v].y, surface[u][v].z);
          vertex(surface[u + 1][v].x, surface[u + 1][v].y, surface[u + 1][v].z);
      }
      endShape();
    }
    
    strokeWeight(0);
    beginShape(QUAD_STRIP);
    
    for(int v = 0; v <= steps; v++)
    {
      fill(
             ((255 / steps) * u) + surface[u][v].x, 
             ((255 / steps) * u) + surface[u][v].y, 
             ((255 / steps) * u) + surface[u][v].z
          );
      if (click == 1)
      {
        vertex(surface[u][v].x, 0, surface[u][v].z);
        vertex(surface[u + 1][v].x, 0, surface[u + 1][v].z);
      }
      if (click == 2)
      {
        vertex(surface[u][v].y, 0, surface[u][v].x);
        vertex(surface[u + 1][v].y, 0, surface[u + 1][v].x);
      }
      if (click == 3)
      {
        vertex(surface[u][v].y, 0,surface[u][v].z);
        vertex(surface[u + 1][v].y, 0,surface[u + 1][v].z);
      }
    }
    
    endShape();
    strokeWeight(1);
  }
}
