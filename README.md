#### 과제 시작전 생각 다이어그램


![스크린샷 2025-02-20 144815](https://github.com/user-attachments/assets/750f605c-8713-4f1e-9d80-86d050b6bffb)



#### 만들어진 스크립트


![스크린샷 2025-02-20 150252](https://github.com/user-attachments/assets/d68b83b3-2f11-44fb-8af3-a85fc198f292)


https://www.figma.com/board/oZmtw5eqbfZ1BvNXhxqaxB/Sparta_KDHMetaverse?node-id=0-1&p=f&t=OShdrFvtFeTwZviW-0




### metaverse


![스크린샷 2025-02-20 144451](https://github.com/user-attachments/assets/bfaca3b1-f8ee-4ccb-997b-c18824a67750)


![스크린샷 2025-02-20 144458](https://github.com/user-attachments/assets/cbe2be21-1154-4a66-9c8d-aabdc21cc3d3)


![스크린샷 2025-02-20 144508](https://github.com/user-attachments/assets/3eaed234-611e-4335-bb2b-5fd9bfd2c527)


![스크린샷 2025-02-20 144514](https://github.com/user-attachments/assets/3987783d-e4c9-45c0-96f7-571c269ccf47)


![스크린샷 2025-02-20 012830](https://github.com/user-attachments/assets/9f4674b2-db24-4146-809e-3f3207510b87)




### FlappyPlane


![스크린샷 2025-02-20 023442](https://github.com/user-attachments/assets/0fa5e5d2-e736-4ef1-865a-7e84285a4522)


![스크린샷 2025-02-20 013703](https://github.com/user-attachments/assets/eb6b1a0b-ef81-4772-b7b3-d3fbdae837dc)


![스크린샷 2025-02-20 013712](https://github.com/user-attachments/assets/24f38ac4-f9e0-4cba-ae20-1da7d0e0c867)




### TheStack


![스크린샷 2025-02-20 023451](https://github.com/user-attachments/assets/e4172569-a599-4161-b0f5-a37735ddd07d)


![스크린샷 2025-02-20 013725](https://github.com/user-attachments/assets/520915f8-c633-4ef3-acd9-4a9974f9c184)


![스크린샷 2025-02-20 013731](https://github.com/user-attachments/assets/e6005d8b-9c33-4d10-a070-f30abadbb106)



## 스크립트 설명

### UI

MetaUI - 외형변경, npc대화

TSUI, FPUI - 시작화면, 스코어, 게임오버화면


### Manager

#### UIManager

UI에 대한 전반적인 관리


#### GameManager

Game에 대한 전반적인 관리


#### DataManager

Data에 대한 전반적인 관리


### AnimationHandler

Meta와 FP에서 플레이어 애니메이션 관리


### FollowCamera

플레이어를 쫓아 오는 카메라 제어


### Controller

#### Meta

MetaPlayer - 플레이어 스킨관련

metaPlayerController - 플레이어 움직중심

FPSceneLoad - FP씬으로 이동

TSSceneLoad - TS씬으로 이동


#### FP

FPPlaneController - fp플레이어 관련

FPObstacle - 장애물 관련

FPBackGround - 장애물 및 배경 반복 관련


####TS

TSBlockController - TS블록관련

TSDestroyZone - TS블록 없애기 위

