extends Area2D

#constant vs var
#constant variables are variables that once loaded can't be changed.
#normal variables or var can be changed at any time.

#preloading the default sprite of the pipe.
const _default = preload("res://CamDev/Sprites/TeaCart/pipe.png")
#preloads the highlighted sprite of the pipe. 
const _highlight = preload("res://CamDev/Sprites/TeaCart/pipe_selected.png")


func _on_Pipe_mouse_entered():
	#Sprite texture set to highlighted texture
	$Sprite.texture = _highlight


func _on_Pipe_mouse_exited():
	#Resets texture back to default texture.
	$Sprite.texture = _default


func _on_Pipe_input_event(viewport, event, shape_idx):
	#if we click on the fuacet
	if Input.is_action_pressed("click"):
		#we check if the faucet animation is pouring, 
		#if it is not:
		if !$FaucetPouring.playing:
			#We play the faucet pouring animation
			$FaucetPouring.play("FaucetPouring")


func _on_FaucetPouring_animation_finished():
	#once the animation is finished, 
	#We set the frame of the animation to zero, so it doesn't get stuck the next time. 
	$FaucetPouring.frame = 0
	#then we stop the animation.
	$FaucetPouring.stop()
