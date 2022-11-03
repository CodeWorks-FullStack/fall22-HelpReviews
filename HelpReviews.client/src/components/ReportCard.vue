<template>
  <div class="row bg-light shadow p-4 rounded">
    <div class="col-2">
      <img :src="report.creator?.picture" :alt="report.creator?.name" class="img-fluid rounded-circle">
    </div>
    <div class="col-8">
      <div>{{ report.creator?.name }}</div>
      <div>Rating: {{ report.rating }}</div>
    </div>
    <div class="col-2">
      <p>{{ new Date(report.createdAt).toLocaleDateString() }}</p>
    </div>
    <div class="col-12 mt-3">
      <strong class="fs-4">{{ report.title }}</strong>
    </div>
    <div class="col-12">
      <p>{{ report.body }}</p>
    </div>
  </div>
</template>


<script>
export default {
  props: {
    report: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>

</style>