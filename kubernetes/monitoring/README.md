# Monitoring with Prometheus

## Kind config
```yaml
kind: Cluster
apiVersion: kind.x-k8s.io/v1alpha4
nodes:
- role: control-plane
- role: worker
- role: worker
```

## Create the cluster
```bash
kind create cluster --name monitoring --image kindest/node:v1.32.0 --config kind-config.yaml
```

## Apply Namespace and CRDs (Custom Resource Definitions)
```bash
kubectl apply --server-side -f manifests/setup    
```

## Wait all CRDs to be created and available
```bash
kubectl wait --for condition=Established --all CustomResourceDefinition --namespace=monitoring
```

## Create the remaining resources
```bash
kubectl apply --server-side -f manifests/   
```

## Forward Grafana port
```bash
kubectl -n monitoring port-forward svc/grafana 3000
```

## Forward Prometheus port
```bash
kubectl -n monitoring port-forward svc/prometheus-operated 9090
```

## Delete the cluster
```bash
kind delete cluster --name monitoring
```