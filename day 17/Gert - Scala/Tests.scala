package main.scala.y2017.Day17

import main.scala.TestBase

class Tests extends TestBase {
  test("Example 1") { Runner.run(3) shouldBe 638 }

  test("Puzzle 1") { Runner.run(314) shouldBe 355 }

  test("Puzzle 2") { Runner.run2(314) shouldBe 6154117 }
}
