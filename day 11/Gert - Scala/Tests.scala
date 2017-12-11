package main.scala.y2017.Day11

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1.1") { Runner.run("ne,ne,ne") shouldBe 3 }
  test("Example 1.2") { Runner.run("ne,ne,sw,sw") shouldBe 0 }
  test("Example 1.3") { Runner.run("ne,ne,s,s") shouldBe 2 }
  test("Example 1.4") { Runner.run("se,sw,se,sw,sw") shouldBe 3 }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day11/puzzle.txt").getLines().mkString("")
    Runner.run(input) shouldBe 877
  }

  test("Example 2.1") { Runner.run2("ne,ne,ne") shouldBe 3 }
  test("Example 2.2") { Runner.run2("ne,ne,sw,sw") shouldBe 2 }
  test("Example 2.3") { Runner.run2("ne,ne,s,s") shouldBe 2 }
  test("Example 2.4") { Runner.run2("se,sw,se,sw,sw") shouldBe 3 }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day11/puzzle.txt").getLines().mkString("")
    Runner.run2(input) shouldBe 1622 // 1623 is wrong
  }
}