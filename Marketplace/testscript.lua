function Update()
  if car:getVelocity().x < 10 then
    car:accelerate()
  end
  if car:getDistanceToLeftSide() > car:getDistanceToRightSide() then
    car:turnLeft()
  else
    car:turnRight()
end