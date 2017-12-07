object Jumps2 extends App {
  val input = Util.readAllLines.map((x) => x.toInt)
  val ints =Array.tabulate(input.length){ i => input(i) + i }
  var cursor = 0
  var result = 0
  while (cursor >= 0 && cursor < ints.length) {
    val index = ints(cursor)
    val offset = index - cursor
    offset match {
      case x if x < 3 => ints(cursor) = index + 1;
      case _ => ints(cursor) = index - 1
    }
    cursor = index
    result += 1
  }
  println(result)
}