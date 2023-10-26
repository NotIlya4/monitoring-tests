resource "helm_release" "metallb" {
  repository = "https://metallb.github.io/metallb"
  chart = "metallb"
  name  = "metallb"

  values = [
    "${file("values.yaml")}"
  ]
}