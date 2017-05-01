function Update()
  if car:getDistanceToLeftSide==car:getDistanceToRightSide then
    car:accelerate()
  else
    if car:getVelocity().x > 1 then
	  car:decelerate()
	end
    if car:getDistanceToLeftSide() > car:getDistanceToRightSide() then
      car:turnLeft()
    else
      car:turnRight()
end