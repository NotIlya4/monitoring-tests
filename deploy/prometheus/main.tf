resource "helm_release" "prometheus" {
  repository = "https://prometheus-community.github.io/helm-charts"
  chart = "kube-prometheus-stack"
  name  = "prometheus"
  
  namespace = "monitoring"
  create_namespace = true

  values = [file("values.yaml")]
}