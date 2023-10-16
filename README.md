```shell
$ minikube addons disable metrics-server
```
```shell
$ minikube start --extra-config=kubelet.authentication-token-webhook=true --extra-config=kubelet.authorization-mode=Webhook --extra-config=scheduler.bind-address=0.0.0.0 --extra-config=controller-manager.bind-address=0.0.0.0
```
```shell
$ helmfile apply -f ./helm/helmfile.yaml
```