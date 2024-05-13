using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTestCamera : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость движения камеры
    public float sensitivity = 2f; // Чувствительность мыши
 
    private void Update()
    {
     // Движение камеры
     float moveHorizontal = Input.GetAxis("Horizontal");
     float moveVertical = Input.GetAxis("Vertical");
     float moveUp = Input.GetKey(KeyCode.Space) ? 1 : 0;
     float moveDown = Input.GetKey(KeyCode.LeftControl) ? -1 : 0;
    
     Vector3 moveDirection = (transform.right * moveHorizontal + transform.forward * moveVertical + Vector3.up * (moveUp + moveDown)).normalized;
     transform.position += moveDirection * moveSpeed * Time.deltaTime;
    
     // Вращение камеры
     float mouseX = Input.GetAxis("Mouse X") * sensitivity;
     float mouseY = -Input.GetAxis("Mouse Y") * sensitivity;
    
     transform.eulerAngles += new Vector3(mouseY, mouseX, 0f);
    }
}
