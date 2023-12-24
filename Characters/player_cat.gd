extends CharacterBody2D

@export var move_speed : float = 100
@export var starting_direction : Vector2 = Vector2(0, -1)
# parameters/idle/blend_position

@onready var animation_tree = $AnimationTree
@onready var state_machine = animation_tree.get("parameters/playback")
# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _ready():
    update_animation_parameters(starting_direction)

func _physics_process(delta):
    var input_direction = Vector2(
        Input.get_action_strength("right")-Input.get_action_strength("left"),
        Input.get_action_strength("down")-Input.get_action_strength("up")
    )
    
    update_animation_parameters(input_direction)
    
    pick_new_state()
    
    #Update velocity
    velocity = input_direction*move_speed
    move_and_slide()

func update_animation_parameters(move_input: Vector2):
    # Don't change animation if there is no input
    if (move_input != Vector2.ZERO):
        animation_tree.set("parameters/Idle/blend_position", move_input)
        animation_tree.set("parameters/Walk/blend_position", move_input)
    
func pick_new_state():
    if (velocity!=Vector2.ZERO):
        state_machine.travel("Walk")
    else:
        state_machine.travel("Idle")


