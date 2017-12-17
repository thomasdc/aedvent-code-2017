package main.scala.y2017.Day16

class Exchange(x: Int, y: Int) extends Move {
  override def act(input: String): String = {
    val tmp = input.charAt(x)
    input.updated(x, input.charAt(y)).updated(y, tmp)
  }
}
