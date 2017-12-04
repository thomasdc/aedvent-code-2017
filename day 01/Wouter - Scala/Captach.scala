object Captach  extends App {
  Util.readAllLines.foreach(crack)

  var result = 0

  def crack(in: String): Unit = {
    val input = in + in.head
    input.sliding(2).foreach(check)
    print(result)
  }

  def check(in: String): Unit = {
    in match {
      case x if x.head.toInt == x.last.toInt => result = result + x.head.toString().toInt
      case _ =>
    }
  }
}
