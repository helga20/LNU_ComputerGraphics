int number_vertices = 10;  //<>//
int vertice_marker_size = 1; 

VoronyiDiagram diag;

void setup()
{
  scale(100);
  size(600, 600);
  smooth();
  diag = new VoronyiDiagram(number_vertices);
}

void draw()
{
  diag.Draw();
  fill(255);
}

class Vertex
{
  float x, y;
  color c;
  PVector velocity;
  Vertex()
  {
    x = random(width + 1);
    y = random(height + 1);
    c = color(random(130), random(130, 255), random(255));
  }

  void move()
  {
    x += velocity.x;
    y += velocity.y;
    if ((x < 0) || (x > width))
    {
      velocity.x *= -1;
    }
    if ((y < 0) || (y > height))
    {
      velocity.y *= -1;
    }
  }
}

class VoronyiDiagram
{
  Vertex[] vertices;

  VoronyiDiagram(int n)
  {
    if (n <= 0)
    {
      throw new IllegalArgumentException(
        "Number of vertices must be greater than 0");
    }
    vertices = new Vertex[n];
    for (int i = 0; i < n; i++)
    {
      vertices[i] = new Vertex();
    }
  }

  void Draw()
  {
    //малювання областей
    loadPixels();

    for (int i = 0; i < pixels.length; i++)
    {
      int nearest = 0;
      int min_len = (int)dist(0, 0, width, height);
      for (int j = 0; j < vertices.length; j++)
      {
        int len = (int)dist(i % width, i / width, vertices[j].x, vertices[j].y);
        if (len < min_len)
        {
          min_len = len;
          nearest = j;
        }
     }
      pixels[i] = vertices[nearest].c;
    }

    updatePixels();
    
    //малювання вершин
    for (int i = 0; i < vertices.length; i++)
    {
      fill(255, 100);
      stroke(0);
      ellipse(vertices[i].x, vertices[i].y, vertice_marker_size, vertice_marker_size);
    }
  }
}
