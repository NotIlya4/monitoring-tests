module "create_kubeconfig_file" {
  source = "./modules/create_kubeconfig_file"
}

module "kube_prometheus_stack" {
  source = "./modules/kube_prometheus_stack"

  providers = {
    helm = helm.helm
  }
}

module "myapp_deploy" {
  source = "./modules/myapp_deploy/"
  
  providers = {
    helm = helm.helm
  }
}