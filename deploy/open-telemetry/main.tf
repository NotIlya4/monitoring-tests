resource "helm_release" "cert_manager" {
  repository = "https://charts.jetstack.io"
  chart = "cert-manager"
  name  = "cert-manager"
  
  namespace = "cert-manager"
  create_namespace = true

  values = [file("cert-manager-values.yaml")]
}

resource "helm_release" "open_telemetry_operator" {
  repository = "https://open-telemetry.github.io/opentelemetry-helm-charts"
  chart = "opentelemetry-operator"
  name  = "opentelemetry-operator"
  
  namespace = "opentelemetry-operator"
  create_namespace = true
  
  values = [file("open-telemetry-operator-values.yaml")]
  
  depends_on = [helm_release.cert_manager]
}