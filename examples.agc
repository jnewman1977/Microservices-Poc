{"version":1,"type":"collection","title":"Examples","queries":[{"version":1,"type":"window","query":"query users {\n  users {\n    all {\n      userId\n      userName\n      firstName\n      lastName\n    }\n  }\n}","apiUrl":"http://localhost:57596/graphql","variables":"{}","subscriptionUrl":"ws://localhost:57596/graphql","subscriptionConnectionParams":"{}","headers":[{"key":"Accept","value":"application/json","enabled":true},{"key":"Content-Type","value":"application/json","enabled":true},{"key":"","value":"","enabled":true}],"windowName":"All Users","preRequestScript":"","preRequestScriptEnabled":false,"postRequestScript":"","postRequestScriptEnabled":false,"id":"5943b369-7977-4367-a28d-70c902cab967","created_at":1656429137604,"updated_at":1656429137604},{"version":1,"type":"window","query":"query tenants {\n  tenants {\n    all {\n      tenantId\n      name\n    }\n  }\n}","apiUrl":"http://localhost:57596/graphql","variables":"{}","subscriptionUrl":"ws://localhost:57596/graphql","subscriptionConnectionParams":"{}","headers":[{"key":"Accept","value":"application/json","enabled":true},{"key":"Content-Type","value":"application/json","enabled":true},{"key":"","value":"","enabled":true}],"windowName":"All Tenants","preRequestScript":"","preRequestScriptEnabled":false,"postRequestScript":"","postRequestScriptEnabled":false,"id":"9f17bc61-a842-433b-869b-e9513bcb8e78","created_at":1656429156334,"updated_at":1656429156334},{"version":1,"type":"window","query":"query user($userId: String) {\n  users {\n    byUserId(userId: $userId) {\n      userId\n      userName\n      firstName\n      lastName\n    }\n  }\n}","apiUrl":"http://localhost:57596/graphql","variables":"{\n  \"userId\": \"624af41f856c8dc998fee99a\"\n}","subscriptionUrl":"ws://localhost:57596/graphql","subscriptionConnectionParams":"{}","headers":[{"key":"Accept","value":"application/json","enabled":true},{"key":"Content-Type","value":"application/json","enabled":true},{"key":"","value":"","enabled":true}],"windowName":"User By Id","preRequestScript":"","preRequestScriptEnabled":false,"postRequestScript":"","postRequestScriptEnabled":false,"id":"58d8b152-9895-4e21-b88f-b4615b295a10","created_at":1656446508871,"updated_at":1656446508871}],"preRequest":{"script":"","enabled":false},"postRequest":{"script":"","enabled":false},"id":"eaf9943f-1b0b-4b00-ad5e-ffa85788fc98","parentPath":"","created_at":1656429137604,"updated_at":1656429137604,"collections":[]}