object Jumps1 extends App {
  val input = Util.readAllLines.map((x) => x.toInt)
  val ints =Array.tabulate(input.length){ i => input(i) + i }
  var cursor = 0
  var result = 0
  while (cursor >= 0 && cursor < ints.length) {
    ints(cursor) = ints(cursor) + 1
    cursor = ints(cursor) - 1
    result += 1
  }
  println(result)
}
