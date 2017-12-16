package main.scala.y2017.Day16

class Spin(size: Int) extends Move {
  override def act(input: String): String = {
    input.drop(input.length - size) + input.take(input.length - size)
  }
}
