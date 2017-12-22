package main.scala.y2017.Day22

import main.scala.TestBase
import scala.io.Source

class Tests extends TestBase {
  test("Example 1") { Runner.run(7) shouldBe 5 }
  test("Example 1.2") { Runner.run(70) shouldBe 41 }
  test("Example 1.3") { Runner.run(10000) shouldBe 5587 }

  test("Puzzle 1") {
    val input = Source.fromFile("test/main/scala/y2017/Day22/puzzle.txt").getLines().toArray
    Runner.run(10000, input) shouldBe 5176
  }

  test("Example 2") { Runner2.run(100) shouldBe 26 }
  test("Example 2.2") { Runner2.run(10000000) shouldBe 2511944 }

  test("Puzzle 2") {
    val input = Source.fromFile("test/main/scala/y2017/Day22/puzzle.txt").getLines().toArray
    Runner2.run(10000000, input) shouldBe 2512017
  }
}
