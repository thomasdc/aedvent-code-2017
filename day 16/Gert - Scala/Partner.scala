package main.scala.y2017.Day16

class Partner(a: Char, b: Char) extends Move {
  override def act(input: String): String = {
    val aPos = input.indexOf(a)
    val bPos = input.indexOf(b)
    input.updated(aPos, b).updated(bPos, a)
  }
}