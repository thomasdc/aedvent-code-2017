object Captach2 extends App {
  Util.readAllLines.foreach(crack)

  var result = 0

  def crack(in: String): Unit = {
    val tmp1 = in.substring(0, in.length / 2)
    val tmp2 = in.substring(in.length /2)
    println(tmp1)
    println(tmp2)
    var x = 0
    tmp1.foreach(l => {
      if(l == tmp2.charAt(x)) {
        result += l.toString.toInt
      }
      x += 1
    })
    println(result * 2)
  }

  def check(in: String): Unit = {
    in match {
      case "" =>
      case x if x.charAt(x.length / 2).toString.equals(x.head) => {
        println(x)
        result += x.head.toString.toInt
        crack(x.substring(1, x.length / 2 -1) + x.substring(x.length /2 +1))
      }
      case x if x.length == 2 && x.charAt(1).toString.equals(x.head) => result += x.head.toString.toInt
      case x if x.length == 2 =>
      case x => crack(x.substring(1, x.length / 2 -1) + x.substring(x.length /2 +1))
    }
  }
}
