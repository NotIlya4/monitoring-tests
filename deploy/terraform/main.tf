resource "null_resource" "generate_kubeconfig" {
  provisioner "local-exec" {
    command = "kubectl config view > kubeconfig"
  }
}

provider "kubernetes" {
  config_path    = "kubeconfig"
  config_context = "minikube"
}

resource "kubernetes_namespace_v1" "test" {
  metadata {
    name = "test"
    annotations = {
      name = "test"
    }
  }
}