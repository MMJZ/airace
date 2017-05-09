angle_lim = 15
speed_lim = 25
distance_lim = 50

function Update()
    if car:getDistanceToNextNode() > distance_lim then
        if car:getSpeed() < v_lim then car:accelerate()
        elseif car:getSpeed() > 2 * speed_lim then car:decelerate() end
    else
        local angle_diff = car:getFacingAngle() - car:getAngleToNextNode()
        if math.abs(angle_diff) > angle_lim then
            if angle_diff > 0 then car:turnLeft() else car:turnRight() end
        else
            local turn_angle = car:getTurnAngle()
            if turn_angle > 0.5 then car:turnLeft()
            elseif turn_angle < -0.5 then car:turnRight() end
        end
    end
end
