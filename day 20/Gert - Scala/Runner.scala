package main.scala.y2017.Day20

object Runner {
  type Particle = ((Int, Int, Int), (Int, Int, Int), (Int, Int, Int))

  val Pattern = """p=<\s*(-?\d+),\s*(-?\d+),\s*(-?\d+)>, v=<\s*(-?\d+),\s*(-?\d+),\s*(-?\d+)>, a=<\s*(-?\d+),\s*(-?\d+),\s*(-?\d+)>""".r

  def run(input: Array[String]): Int = {
    input.map(parse).zipWithIndex
      .map(p => (p._2, Math.abs(p._1._3._1) + Math.abs(p._1._3._2) + Math.abs(p._1._3._3)))
      .minBy(particle => particle._2)._1
  }

  def run2(input: Array[String]): Int = {
    var particles = input.map(parse).zipWithIndex

    while(!expectNoMoreCollisions(particles)) {
      particles = particles
        .map(p => // Update speeds
          ((p._1._1, (p._1._2._1 + p._1._3._1, p._1._2._2 + p._1._3._2, p._1._2._3 + p._1._3._3), p._1._3), p._2))
        .map(p => // Move
          (((p._1._1._1 + p._1._2._1, p._1._1._2 + p._1._2._2, p._1._1._3 + p._1._2._3), p._1._2, p._1._3), p._2))
        // Eliminate particles at same position
        .groupBy(p => p._1._1).filter(pGroup => pGroup._2.length == 1).flatMap(_._2).toArray
    }

    particles.length
  }

  def expectNoMoreCollisions(particles: Array[(Particle, Int)]): Boolean = {
    if(particles.length < 2)
      true
    else {
      inOrder(particles.map(p => (p._1._1._1, p._1._2._1, p._1._3._1))) ||
        inOrder(particles.map(p => (p._1._1._2, p._1._2._2, p._1._3._2))) ||
        inOrder(particles.map(p => (p._1._1._3, p._1._2._3, p._1._3._3)))
    }
  }

  // Checks if particles are ordered for a single dimension (position, velocity, acceleration)
  def inOrder(particles: Array[(Int, Int, Int)]): Boolean =
    particles.sortBy(p => p._1).sliding(2).forall(pair => pair(0)._2 <= pair(1)._2 && pair(0)._3 <= pair(1)._3)

  def parse(input: String): Particle = {
    input match {
      case Pattern(px, py, pz, vx, vy, vz, ax, ay, az) =>
        ((px.toInt, py.toInt, pz.toInt), (vx.toInt, vy.toInt, vz.toInt), (ax.toInt, ay.toInt, az.toInt))
      case _ => throw new Exception("Couldn't parse particle %s".format(input))
    }
  }
}
