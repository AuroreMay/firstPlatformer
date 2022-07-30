using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Animator playerAnimator;
    //variavel privada do tipo animator (componente) para pegar o componente animator
    private Rigidbody2D playerRigidbody;
    //variavel privada do tipo rigidbody (componente) para pegar o rigidbody
    public Transform groundCheck;
    //variavel publica do tipo transform (componente) para pegar o groudcheck
    public LayerMask isGround;
    //variavel publica do tipo LayerMask (componente) para pegar a layer que quero checar
    public bool Grounded, flip_Sprite, isAttaking;
    //variaveis do tipo booleanos publica
    public float speed,jump_force,h,v;
    public int id_Anim;
    //variavel publica para pegar o id de animação do animator
    // Start is called before the first frame update
    void Start()
    {
        //getComponent serve para pegar o anime do GameObject do script
        playerAnimator = GetComponent<Animator>(); 
        playerRigidbody = GetComponent<Rigidbody2D>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        //através do Input.GetAxisRaw é possível pegar o input de comandos para mexer h e v
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //checando se meu h é maior ou menor e seu sprite está virado ou não
        if (h > 0 && flip_Sprite == true && isAttaking == false) {
            flipSprite();
        }
        else if (h < 0 && flip_Sprite == false && isAttaking == false) {
            flipSprite();
        }
        //checando se h é ou não diferente de zero para mudar a animação
        if (h != 0){
            id_Anim = 1;
        }
        else{
            id_Anim = 0;
        }
        if (Input.GetButtonDown("Fire1") && isAttaking == false){
            playerAnimator.SetTrigger("attack");
        }
        //checando o pulo para ver se o personagem pula ou não
        if (Input.GetButtonDown("Jump") && Grounded == true){
            playerRigidbody.AddForce(new Vector2(0,jump_force));
        }
        if (isAttaking == true && Grounded == true){
            h = 0;
            
        }
        /* usando a variavel do tipo animator definida anteriormente é possível usar "Set" para pegar um parâmetro
        do animator e definir como variável */
        playerAnimator.SetBool("grounded", Grounded);
        playerAnimator.SetInteger("id_Animation",id_Anim);
    }
    //usando fixedUpdate pois ele tem uma taxa fixa de atualização
    void FixedUpdate() {
        //usando Pyssics2D.OverlapCircle é possível checar se o meu ground toca o chão ou não
        Grounded = Physics2D.OverlapCircle(groundCheck.position,-0.07f,isGround);
        //alterando a velocidade do player
        playerRigidbody.velocity = new Vector2(h * speed, playerRigidbody.velocity.y);
        //certificando que não rodará
        playerRigidbody.freezeRotation = true;
    }
    
    void flipSprite(){
        flip_Sprite = !flip_Sprite;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x,transform.localScale.y,transform.localScale.z);
    }
    
    public void isAttack(int atk){
        switch (atk)
        {
            case 0: 
                isAttaking = false;
                break;
            case 1:
                isAttaking = true;
                break;
            default:
                isAttaking = false;
                break;
    
        }
    }
}
