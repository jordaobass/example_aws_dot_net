# Projeto para samples de servicos AWS 
### Docker compose com configuracoes necessarias para testar 

___
## criar SNS
```shell
aws --endpoint-url=http://localhost:4566 sns create-topic --name order-creation-events --region eu-central-1 --profile test-profile --output table | cat
```

## Criar SQS
```shell
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name dummy-queue --profile test-profile --region eu-central-1 --output table | cat
```
___ 


### Enviar Mensagens SQS
```shell
aws --endpoint-url=http://localhost:4566 sqs send-message  --queue-url http://localhost:4566/000000000000/dummy-queue --profile test-profile --region eu-central-1  --message-body '{
          "event_id": "7456c8ee-949d-4100-a0c6-6ae8e581ae15",
          "event_time": "2019-11-26T16:00:47Z",
          "data": {
            "test": 83411
        }
      }' | cat
```

### Ler mensagens SQS
```shell
aws --endpoint-url=http://localhost:4566 sqs receive-message --queue-url http://localhost:4566/000000000000/dummy-queue --profile test-profile --region eu-central-1 --output json | cat
```

### Excluir mensagens  SQS
```shell
aws sqs delete-message --endpoint-url=http://localhost:4566 --queue-url http://localhost:4566/000000000000/dummy-queue --profile localstack --region eu-central-1 -- identificador de recibo < identificador de mensagem>
```

### Incluir SNS
```shell
aws sns publish --endpoint-url=http://localhost:4566 --topic-arn arn:aws:sns:eu-central-1:000000000000:order-creation-events --message "Hello World" --profile test-profile --region eu-central-1 --output json | cat
```