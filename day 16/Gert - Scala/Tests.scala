package main.scala.y2017.Day16

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") {
    new Runner().run(Array("s1", "x3/4", "pe/b"), 1, 5) shouldBe "baedc"
  }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day16/puzzle.txt")
      .getLines().mkString("").split(",")
    new Runner().run(input) shouldBe "padheomkgjfnblic"
  }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day16/puzzle.txt")
      .getLines().mkString("").split(",")
    new Runner().run(input, 1000000000) shouldBe "bfcdeakhijmlgopn"
  }
}
